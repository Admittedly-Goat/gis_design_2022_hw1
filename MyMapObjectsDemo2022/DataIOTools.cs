using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MyMapObjectsDemo2022
{
    internal static class DataIOTools
    {
        #region 程序集方法

        internal static MyMapObjects.moMapLayer LoadMapLayer(BinaryReader sr, String filePath)
        {
            //由于GeoJSON的定义，所有数据文件都需要用WGS84经纬度来存储坐标。Lay文件使用Lambert投影存储坐标，所以需要转化成经纬度后显示
            MyMapObjects.moProjectionCS projectionLay = layCoordinateTranslateToWGS1984Object();
            Int32 sTemp = sr.ReadInt32();   //不需要
            MyMapObjects.moGeometryTypeConstant sGeometryType = (MyMapObjects.moGeometryTypeConstant)sr.ReadInt32();
            MyMapObjects.moFields sFields = LoadFields(sr);
            MyMapObjects.moFeatures sFeatures = LoadFeatures(sGeometryType, sFields, sr, projectionLay);
            MyMapObjects.moMapLayer sMapLayer = new MyMapObjects.moMapLayer(Path.GetFileName(filePath), sGeometryType, sFields);
            sMapLayer.Features = sFeatures;
            return sMapLayer;
        }

        internal static MyMapObjects.moMapLayer LoadMapLayerFromGeoJSON(String fileLocation)
        {
            //检查用户输入
            dynamic geojsonData = JsonConvert.DeserializeObject(File.ReadAllText(fileLocation));
            if (geojsonData.features.Count == 0)
            {
                throw new Exception("用户输入了一个空的图层，不含任何的属性");
            }

            //根据Lay文件解析方法，声明变量
            MyMapObjects.moFields sFields = new MyMapObjects.moFields();
            MyMapObjects.moFeatures sFeatures = new MyMapObjects.moFeatures();
            MyMapObjects.moGeometryTypeConstant geomType;
            bool isMultiLineString = false;

            //检查图层类型
            if (geojsonData.features[0].geometry.type == "Point")
            {
                geomType = MyMapObjects.moGeometryTypeConstant.Point;
            }
            else if ((geojsonData.features[0].geometry.type == "LineString") || (geojsonData.features[0].geometry.type == "MultiLineString"))
            {
                if (geojsonData.features[0].geometry.type == "MultiLineString")
                {
                    isMultiLineString = true;
                }
                geomType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
            }
            else if (geojsonData.features[0].geometry.type == "Polygon")
            {
                geomType = MyMapObjects.moGeometryTypeConstant.MultiPolygon;
            }
            else
            {
                throw new Exception("用户输入的几何类型不在本程序所支持的GeoJSON子集之内");
            }

            //生成字段
            foreach (dynamic i in geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>().Keys)
            {
                int sValueType = -1;
                if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int16))
                {
                    sValueType = 0;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int32))
                {
                    sValueType = 1;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int64))
                {
                    sValueType = 2;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(float))
                {
                    sValueType = 3;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(double))
                {
                    sValueType = 4;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(String))
                {
                    sValueType = 5;
                }
                else
                {
                    throw new Exception("无法识别本程序所支持GeoJSON子集的属性信息类型");
                }
                MyMapObjects.moField sField = new MyMapObjects.moField(i, (MyMapObjects.moValueTypeConstant)sValueType);
                sFields.Append(sField);
            }

            //解析每个要素
            foreach (dynamic i in geojsonData.features)
            {
                MyMapObjects.moGeometry geomData;
                if (geomType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(i.geometry.coordinates[0].Value, i.geometry.coordinates[1].Value);
                    geomData = sPoint;
                }
                else if (geomType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
                    if (isMultiLineString)
                    {
                        foreach (dynamic j in i.geometry.coordinates)
                        {
                            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                            foreach (dynamic k in j)
                            {
                                sPoints.Add(new MyMapObjects.moPoint(k[0].Value, k[1].Value));
                            }
                            sMultiPolyline.Parts.Add(sPoints);
                        }
                    }
                    else
                    {
                        MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                        foreach (dynamic j in i.geometry.coordinates)
                        {
                            sPoints.Add(new MyMapObjects.moPoint(j[0].Value, j[1].Value));
                        }
                        sMultiPolyline.Parts.Add(sPoints);
                    }
                    sMultiPolyline.UpdateExtent();
                    geomData = sMultiPolyline;
                }
                else if (geomType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
                    foreach (dynamic j in i.geometry.coordinates)
                    {
                        MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                        foreach (dynamic k in j)
                        {
                            sPoints.Add(new MyMapObjects.moPoint(k[0].Value, k[1].Value));
                        }
                        sMultiPolygon.Parts.Add(sPoints);
                    }
                    sMultiPolygon.UpdateExtent();
                    geomData = sMultiPolygon;
                }
                else
                {
                    throw new Exception("用户输入的地理特征数据不在本程序支持的自己范围内，而且绕过了之前的检查");
                }

                MyMapObjects.moAttributes sAttributes = new MyMapObjects.moAttributes();
                Dictionary<String, dynamic> jsonPropDict = i.properties.ToObject<Dictionary<string, dynamic>>();
                for (int j = 0; j < sFields.Count; j++)
                {
                    MyMapObjects.moField sField = sFields.GetItem(j);
                    object sValue = jsonPropDict[sField.Name];
                    sAttributes.Append(sValue);
                }
                MyMapObjects.moFeature sFeature = new MyMapObjects.moFeature(geomType, geomData, sAttributes);
                sFeatures.Add(sFeature);
            }
            MyMapObjects.moMapLayer sMapLayer = new MyMapObjects.moMapLayer(Path.GetFileName(fileLocation), geomType, sFields);
            sMapLayer.Features = sFeatures;
            return sMapLayer;
        }

        internal static void SaveLayerAsGeoJSON(string fileLocation, MyMapObjects.moMapLayer layer)
        {
            List<string> geojsonPropertyNameList = new List<string>();
            List<MyMapObjects.moValueTypeConstant> geojsonPropertyValueList = new List<MyMapObjects.moValueTypeConstant>();
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                geojsonPropertyNameList.Add(layer.AttributeFields.GetItem(i).Name);
                geojsonPropertyValueList.Add(layer.AttributeFields.GetItem(i).ValueType);
            }
            Dictionary<String, dynamic> geojsonDict = new Dictionary<string, dynamic>();
            geojsonDict["type"] = "FeatureCollection";
            List<dynamic> geojsonFeatureList = new List<dynamic>();
            geojsonDict["features"] = geojsonFeatureList;
            for (int i = 0; i < layer.Features.Count; i++)
            {
                Dictionary<string, dynamic> singleFeatureDict = new Dictionary<string, dynamic>();
                geojsonFeatureList.Add(singleFeatureDict);
                singleFeatureDict["type"] = "Feature";
                singleFeatureDict["id"] = i;
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","Point" },
                        {"coordinates",new List<double>(){((MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry).X, ((MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry).Y} }
                    };
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","MultiLineString" }
                    };
                    List<dynamic> coordinatePartList = new List<dynamic>();
                    var geomPolyline = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                    for (int j = 0; j < geomPolyline.Parts.Count; j++)
                    {
                        List<dynamic> coordinatePointList = new List<dynamic>();
                        for (int k = 0; k < geomPolyline.Parts.GetItem(j).Count; k++)
                        {
                            List<double> coordinate = new List<double>() { geomPolyline.Parts.GetItem(j).GetItem(k).X, geomPolyline.Parts.GetItem(j).GetItem(k).Y };
                            coordinatePointList.Add(coordinate);
                        }
                        coordinatePartList.Add(coordinatePointList);
                    }
                    singleFeatureDict["geometry"]["coordinates"] = coordinatePartList;
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","Polygon" }
                    };
                    List<dynamic> coordinatePartList = new List<dynamic>();
                    var geomPolygon = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                    for (int j = 0; j < geomPolygon.Parts.Count; j++)
                    {
                        List<dynamic> coordinatePointList = new List<dynamic>();
                        for (int k = 0; k < geomPolygon.Parts.GetItem(j).Count; k++)
                        {
                            List<double> coordinate = new List<double>() { geomPolygon.Parts.GetItem(j).GetItem(k).X, geomPolygon.Parts.GetItem(j).GetItem(k).Y };
                            coordinatePointList.Add(coordinate);
                        }
                        coordinatePartList.Add(coordinatePointList);
                    }
                    singleFeatureDict["geometry"]["coordinates"] = coordinatePartList;
                }
                Dictionary<string, dynamic> propertyDict = new Dictionary<string, dynamic>();
                singleFeatureDict["properties"] = propertyDict;
                for (int j = 0; j < geojsonPropertyValueList.Count; j++)
                {
                    if ((geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt16) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt32) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt64))
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToInt64(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                    else if ((geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dDouble) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dSingle))
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToDouble(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                    else if (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dText)
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToString(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                }
            }
            string json = JsonConvert.SerializeObject(geojsonDict);
            File.WriteAllText(fileLocation, json);
        }
        #endregion

        #region 私有函数
        //生成Lay文件解析时使用的坐标转换对象
        private static MyMapObjects.moProjectionCS layCoordinateTranslateToWGS1984Object()
        {
            string sProjCSName = "Beijing54 Lambert Conformal Conic 2SP";
            string sGeoCSName = "Beijing 1954";
            string sDatumName = "Beijing 1954";
            string sSpheroidName = "Krassowsky_1940";
            double sSemiMajor = 6378245;
            double sInverseFlattening = 298.3;
            double sOriginLatitude = 0;
            double sCentralMeridian = 105;
            double sFalseEasting = 0;
            double sFalseNorthing = 0;
            double sScaleFactor = 1;
            double sStandardParallelOne = 30;
            double sStandardParallelTwo = 62;
            MyMapObjects.moLinearUnitConstant sLinearUnit = MyMapObjects.moLinearUnitConstant.Meter;
            MyMapObjects.moProjectionTypeConstant sProjType = MyMapObjects.moProjectionTypeConstant.Lambert_Conformal_Conic_2SP;
            return new MyMapObjects.moProjectionCS(sProjCSName, sGeoCSName, sDatumName, sSpheroidName, sSemiMajor,
                sInverseFlattening, sProjType, sOriginLatitude, sCentralMeridian, sFalseEasting,
                sFalseNorthing, sScaleFactor, sStandardParallelOne, sStandardParallelTwo, sLinearUnit);
        }
        //读取字段集合
        private static MyMapObjects.moFields LoadFields(BinaryReader sr)
        {
            Int32 sFieldCount = sr.ReadInt32(); //字段数量
            MyMapObjects.moFields sFields = new MyMapObjects.moFields();
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                string sName = sr.ReadString();
                MyMapObjects.moValueTypeConstant sValueType = (MyMapObjects.moValueTypeConstant)sr.ReadInt32();
                Int32 sTemp = sr.ReadInt32();   //不需要；
                MyMapObjects.moField sField = new MyMapObjects.moField(sName, sValueType);
                sFields.Append(sField);
            }
            return sFields;
        }

        //读取要素集合
        private static MyMapObjects.moFeatures LoadFeatures(MyMapObjects.moGeometryTypeConstant geometryType, MyMapObjects.moFields fields, BinaryReader sr, MyMapObjects.moProjectionCS projection)
        {
            MyMapObjects.moFeatures sFeatures = new MyMapObjects.moFeatures();
            Int32 sFeatureCount = sr.ReadInt32();
            for (int i = 0; i <= sFeatureCount - 1; i++)
            {
                MyMapObjects.moGeometry sGeometry = LoadGeometry(geometryType, sr, projection);
                MyMapObjects.moAttributes sAttributes = LoadAttributes(fields, sr);
                MyMapObjects.moFeature sFeature = new MyMapObjects.moFeature(geometryType, sGeometry, sAttributes);
                sFeatures.Add(sFeature);
            }
            return sFeatures;
        }

        private static MyMapObjects.moGeometry LoadGeometry(MyMapObjects.moGeometryTypeConstant geometryType, BinaryReader sr, MyMapObjects.moProjectionCS projection)
        {
            if (geometryType == MyMapObjects.moGeometryTypeConstant.Point)
            {
                MyMapObjects.moPoint sPoint = LoadPoint(sr, projection);
                return sPoint;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
            {
                MyMapObjects.moMultiPolyline sMultiPolyline = LoadMultiPolyline(sr, projection);
                return sMultiPolyline;
            }
            else if (geometryType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
            {
                MyMapObjects.moMultiPolygon sMultiPolygon = LoadMultiPolygon(sr, projection);
                return sMultiPolygon;
            }
            else
                return null;
        }

        //读取一个点
        private static MyMapObjects.moPoint LoadPoint(BinaryReader sr, MyMapObjects.moProjectionCS projection)
        {
            //原数据支持多点，按照多点读取，然后返回多点的第一个点
            Int32 sPointCount = sr.ReadInt32();
            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
            for (Int32 i = 0; i <= sPointCount - 1; i++)
            {
                double sX = sr.ReadDouble();
                double sY = sr.ReadDouble();
                MyMapObjects.moPoint sPoint = projection.TransferToLngLat(new MyMapObjects.moPoint(sX, sY));
                sPoints.Add(sPoint);
            }
            return sPoints.GetItem(0);
        }

        //读取一个复合折线
        private static MyMapObjects.moMultiPolyline LoadMultiPolyline(BinaryReader sr, MyMapObjects.moProjectionCS projection)
        {
            MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
            Int32 sPartCount = sr.ReadInt32();
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                Int32 sPointCount = sr.ReadInt32();
                for (Int32 j = 0; j <= sPointCount - 1; j++)
                {
                    double sX = sr.ReadDouble();
                    double sY = sr.ReadDouble();
                    MyMapObjects.moPoint sPoint = projection.TransferToLngLat(new MyMapObjects.moPoint(sX, sY));
                    sPoints.Add(sPoint);
                }
                sMultiPolyline.Parts.Add(sPoints);
            }
            sMultiPolyline.UpdateExtent();
            return sMultiPolyline;
        }

        //读取一个复合多边形
        private static MyMapObjects.moMultiPolygon LoadMultiPolygon(BinaryReader sr, MyMapObjects.moProjectionCS projection)
        {
            MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
            Int32 sPartCount = sr.ReadInt32();
            for (Int32 i = 0; i <= sPartCount - 1; i++)
            {
                MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                Int32 sPointCount = sr.ReadInt32();
                for (Int32 j = 0; j <= sPointCount - 1; j++)
                {
                    double sX = sr.ReadDouble();
                    double sY = sr.ReadDouble();
                    MyMapObjects.moPoint sPoint = projection.TransferToLngLat(new MyMapObjects.moPoint(sX, sY));
                    sPoints.Add(sPoint);
                }
                sMultiPolygon.Parts.Add(sPoints);
            }
            sMultiPolygon.UpdateExtent();
            return sMultiPolygon;
        }

        private static MyMapObjects.moAttributes LoadAttributes(MyMapObjects.moFields fields, BinaryReader sr)
        {
            Int32 sFieldCount = fields.Count;
            MyMapObjects.moAttributes sAttributes = new MyMapObjects.moAttributes();
            for (Int32 i = 0; i <= sFieldCount - 1; i++)
            {
                MyMapObjects.moField sField = fields.GetItem(i);
                object sValue = LoadValue(sField.ValueType, sr);
                sAttributes.Append(sValue);
            }
            return sAttributes;
        }

        private static object LoadValue(MyMapObjects.moValueTypeConstant valueType, BinaryReader sr)
        {
            if (valueType == MyMapObjects.moValueTypeConstant.dInt16)
            {
                Int16 sValue = sr.ReadInt16();
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dInt32)
            {
                Int32 sValue = sr.ReadInt32();
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dInt64)
            {
                Int64 sValue = sr.ReadInt64();
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dSingle)
            {
                float sValue = sr.ReadSingle();
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dDouble)
            {
                double sValue = sr.ReadDouble();
                return sValue;
            }
            else if (valueType == MyMapObjects.moValueTypeConstant.dText)
            {
                string sValue = sr.ReadString();
                return sValue;
            }
            else
            {
                return null;
            }
        }

        public static void SaveAsTuMuGISProjectFile(MyMapObjects.moMapControl moMap, string fileLocation)
        {
            List<dynamic> rawList = new List<dynamic>();
            for (int i = 0; i < moMap.Layers.Count; i++)
            {
                Dictionary<string, dynamic> newLayer = new Dictionary<string, dynamic>();
                rawList.Add(newLayer);
                MyMapObjects.moMapLayer layer = moMap.Layers.GetItem(i);
                newLayer["Name"] = layer.Name;

                Dictionary<string, dynamic> newAppendixValue = new Dictionary<string, dynamic>();
                newLayer["Appendix"] = newAppendixValue;
                if (layer.LabelRenderer == null)
                {
                    newAppendixValue["Visible"] = false;
                }
                else
                {
                    newAppendixValue["Visible"] = true;
                    newAppendixValue["Field"] = layer.LabelRenderer.Field;
                    newAppendixValue["Font"] = layer.LabelRenderer.TextSymbol.Font.FontFamily;
                    newAppendixValue["FontSize"] = layer.LabelRenderer.TextSymbol.Font.Size;
                    newAppendixValue["Color"] = new List<int>() { layer.LabelRenderer.TextSymbol.FontColor.R, layer.LabelRenderer.TextSymbol.FontColor.G, layer.LabelRenderer.TextSymbol.FontColor.B };
                }

                Dictionary<string, dynamic> newRendererValue = new Dictionary<string, dynamic>();
                newLayer["Renderer"] = newRendererValue;
                var renderer = layer.Renderer;
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
                    {
                        newRendererValue["Type"] = "SimpleRendererPoint";
                        newRendererValue["Color"] = new List<int>() { ((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.R,
                            ((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.G,
                            ((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.B };
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Style);
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
                    {
                        newRendererValue["Type"] = "UniqueRendererPoint";
                        newRendererValue["Field"] = ((MyMapObjects.moUniqueValueRenderer)renderer).Field;
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moUniqueValueRenderer)renderer).DefaultSymbol)).Style);
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
                    {
                        newRendererValue["Type"] = "ClassRendererPoint";
                        newRendererValue["Field"] = ((MyMapObjects.moClassBreaksRenderer)renderer).Field;
                        newRendererValue["Class"] = ((MyMapObjects.moClassBreaksRenderer)renderer).BreakCount;
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleMarkerSymbol)(((MyMapObjects.moClassBreaksRenderer)renderer).DefaultSymbol)).SymbolType);
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
                    {
                        newRendererValue["Type"] = "SimpleRendererPolyline";
                        newRendererValue["Color"] = new List<int>() { ((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.R,
                            ((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.G,
                            ((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.B };
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Style);
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
                    {
                        newRendererValue["Type"] = "UniqueRendererPolyline";
                        newRendererValue["Field"] = ((MyMapObjects.moUniqueValueRenderer)renderer).Field;
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moUniqueValueRenderer)renderer).DefaultSymbol)).Style);
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
                    {
                        newRendererValue["Type"] = "ClassRendererPolyline";
                        newRendererValue["Field"] = ((MyMapObjects.moClassBreaksRenderer)renderer).Field;
                        newRendererValue["Class"] = ((MyMapObjects.moClassBreaksRenderer)renderer).BreakCount;
                        newRendererValue["DrawType"] = (int)(((MyMapObjects.moSimpleLineSymbol)(((MyMapObjects.moClassBreaksRenderer)renderer).DefaultSymbol)).SymbolType);
                    }
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.Simple)
                    {
                        newRendererValue["Type"] = "SimpleRendererPolygon";
                        newRendererValue["Color"] = new List<int>() { ((MyMapObjects.moSimpleFillSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.R,
                            ((MyMapObjects.moSimpleFillSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.G,
                            ((MyMapObjects.moSimpleFillSymbol)(((MyMapObjects.moSimpleRenderer)renderer).Symbol)).Color.B };
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.UniqueValue)
                    {
                        newRendererValue["Type"] = "UniqueRendererPolygon";
                        newRendererValue["Field"] = ((MyMapObjects.moUniqueValueRenderer)renderer).Field;
                    }
                    else if (renderer.RendererType == MyMapObjects.moRendererTypeConstant.ClassBreaks)
                    {
                        newRendererValue["Type"] = "ClassRendererPolygon";
                        newRendererValue["Field"] = ((MyMapObjects.moClassBreaksRenderer)renderer).Field;
                        newRendererValue["Class"] = ((MyMapObjects.moClassBreaksRenderer)renderer).BreakCount;
                    }
                }

                newLayer["GeoJSONStr"] = SaveLayerAsGeoJSONString(layer);
            }

            string json = JsonConvert.SerializeObject(rawList);
            File.WriteAllText(fileLocation, json);
        }

        private static MyMapObjects.moMapLayer LoadMapLayerFromGeoJSONByGeoJSONString(String json, String layerName)
        {
            //检查用户输入
            dynamic geojsonData = JsonConvert.DeserializeObject(json);
            if (geojsonData.features.Count == 0)
            {
                throw new Exception("用户输入了一个空的图层，不含任何的属性");
            }

            //根据Lay文件解析方法，声明变量
            MyMapObjects.moFields sFields = new MyMapObjects.moFields();
            MyMapObjects.moFeatures sFeatures = new MyMapObjects.moFeatures();
            MyMapObjects.moGeometryTypeConstant geomType;
            bool isMultiLineString = false;

            //检查图层类型
            if (geojsonData.features[0].geometry.type == "Point")
            {
                geomType = MyMapObjects.moGeometryTypeConstant.Point;
            }
            else if ((geojsonData.features[0].geometry.type == "LineString") || (geojsonData.features[0].geometry.type == "MultiLineString"))
            {
                if (geojsonData.features[0].geometry.type == "MultiLineString")
                {
                    isMultiLineString = true;
                }
                geomType = MyMapObjects.moGeometryTypeConstant.MultiPolyline;
            }
            else if (geojsonData.features[0].geometry.type == "Polygon")
            {
                geomType = MyMapObjects.moGeometryTypeConstant.MultiPolygon;
            }
            else
            {
                throw new Exception("用户输入的几何类型不在本程序所支持的GeoJSON子集之内");
            }

            //生成字段
            foreach (dynamic i in geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>().Keys)
            {
                int sValueType = -1;
                if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int16))
                {
                    sValueType = 0;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int32))
                {
                    sValueType = 1;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(Int64))
                {
                    sValueType = 2;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(float))
                {
                    sValueType = 3;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(double))
                {
                    sValueType = 4;
                }
                else if (geojsonData.features[0].properties.ToObject<Dictionary<string, dynamic>>()[i].GetType() == typeof(String))
                {
                    sValueType = 5;
                }
                else
                {
                    throw new Exception("无法识别本程序所支持GeoJSON子集的属性信息类型");
                }
                MyMapObjects.moField sField = new MyMapObjects.moField(i, (MyMapObjects.moValueTypeConstant)sValueType);
                sFields.Append(sField);
            }

            //解析每个要素
            foreach (dynamic i in geojsonData.features)
            {
                MyMapObjects.moGeometry geomData;
                if (geomType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    MyMapObjects.moPoint sPoint = new MyMapObjects.moPoint(i.geometry.coordinates[0].Value, i.geometry.coordinates[1].Value);
                    geomData = sPoint;
                }
                else if (geomType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    MyMapObjects.moMultiPolyline sMultiPolyline = new MyMapObjects.moMultiPolyline();
                    if (isMultiLineString)
                    {
                        foreach (dynamic j in i.geometry.coordinates)
                        {
                            MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                            foreach (dynamic k in j)
                            {
                                sPoints.Add(new MyMapObjects.moPoint(k[0].Value, k[1].Value));
                            }
                            sMultiPolyline.Parts.Add(sPoints);
                        }
                    }
                    else
                    {
                        MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                        foreach (dynamic j in i.geometry.coordinates)
                        {
                            sPoints.Add(new MyMapObjects.moPoint(j[0].Value, j[1].Value));
                        }
                        sMultiPolyline.Parts.Add(sPoints);
                    }
                    sMultiPolyline.UpdateExtent();
                    geomData = sMultiPolyline;
                }
                else if (geomType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    MyMapObjects.moMultiPolygon sMultiPolygon = new MyMapObjects.moMultiPolygon();
                    foreach (dynamic j in i.geometry.coordinates)
                    {
                        MyMapObjects.moPoints sPoints = new MyMapObjects.moPoints();
                        foreach (dynamic k in j)
                        {
                            sPoints.Add(new MyMapObjects.moPoint(k[0].Value, k[1].Value));
                        }
                        sMultiPolygon.Parts.Add(sPoints);
                    }
                    sMultiPolygon.UpdateExtent();
                    geomData = sMultiPolygon;
                }
                else
                {
                    throw new Exception("用户输入的地理特征数据不在本程序支持的自己范围内，而且绕过了之前的检查");
                }

                MyMapObjects.moAttributes sAttributes = new MyMapObjects.moAttributes();
                Dictionary<String, dynamic> jsonPropDict = i.properties.ToObject<Dictionary<string, dynamic>>();
                for (int j = 0; j < sFields.Count; j++)
                {
                    MyMapObjects.moField sField = sFields.GetItem(j);
                    object sValue = jsonPropDict[sField.Name];
                    sAttributes.Append(sValue);
                }
                MyMapObjects.moFeature sFeature = new MyMapObjects.moFeature(geomType, geomData, sAttributes);
                sFeatures.Add(sFeature);
            }
            MyMapObjects.moMapLayer sMapLayer = new MyMapObjects.moMapLayer(layerName, geomType, sFields);
            sMapLayer.Features = sFeatures;
            return sMapLayer;
        }

        private static String SaveLayerAsGeoJSONString(MyMapObjects.moMapLayer layer)
        {
            List<string> geojsonPropertyNameList = new List<string>();
            List<MyMapObjects.moValueTypeConstant> geojsonPropertyValueList = new List<MyMapObjects.moValueTypeConstant>();
            for (int i = 0; i < layer.AttributeFields.Count; i++)
            {
                geojsonPropertyNameList.Add(layer.AttributeFields.GetItem(i).Name);
                geojsonPropertyValueList.Add(layer.AttributeFields.GetItem(i).ValueType);
            }
            Dictionary<String, dynamic> geojsonDict = new Dictionary<string, dynamic>();
            geojsonDict["type"] = "FeatureCollection";
            List<dynamic> geojsonFeatureList = new List<dynamic>();
            geojsonDict["features"] = geojsonFeatureList;
            for (int i = 0; i < layer.Features.Count; i++)
            {
                Dictionary<string, dynamic> singleFeatureDict = new Dictionary<string, dynamic>();
                geojsonFeatureList.Add(singleFeatureDict);
                singleFeatureDict["type"] = "Feature";
                singleFeatureDict["id"] = i;
                if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.Point)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","Point" },
                        {"coordinates",new List<double>(){((MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry).X, ((MyMapObjects.moPoint)layer.Features.GetItem(i).Geometry).Y} }
                    };
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolyline)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","MultiLineString" }
                    };
                    List<dynamic> coordinatePartList = new List<dynamic>();
                    var geomPolyline = (MyMapObjects.moMultiPolyline)layer.Features.GetItem(i).Geometry;
                    for (int j = 0; j < geomPolyline.Parts.Count; j++)
                    {
                        List<dynamic> coordinatePointList = new List<dynamic>();
                        for (int k = 0; k < geomPolyline.Parts.GetItem(j).Count; k++)
                        {
                            List<double> coordinate = new List<double>() { geomPolyline.Parts.GetItem(j).GetItem(k).X, geomPolyline.Parts.GetItem(j).GetItem(k).Y };
                            coordinatePointList.Add(coordinate);
                        }
                        coordinatePartList.Add(coordinatePointList);
                    }
                    singleFeatureDict["geometry"]["coordinates"] = coordinatePartList;
                }
                else if (layer.ShapeType == MyMapObjects.moGeometryTypeConstant.MultiPolygon)
                {
                    singleFeatureDict["geometry"] = new Dictionary<string, dynamic>()
                    {
                        {"type","Polygon" }
                    };
                    List<dynamic> coordinatePartList = new List<dynamic>();
                    var geomPolygon = (MyMapObjects.moMultiPolygon)layer.Features.GetItem(i).Geometry;
                    for (int j = 0; j < geomPolygon.Parts.Count; j++)
                    {
                        List<dynamic> coordinatePointList = new List<dynamic>();
                        for (int k = 0; k < geomPolygon.Parts.GetItem(j).Count; k++)
                        {
                            List<double> coordinate = new List<double>() { geomPolygon.Parts.GetItem(j).GetItem(k).X, geomPolygon.Parts.GetItem(j).GetItem(k).Y };
                            coordinatePointList.Add(coordinate);
                        }
                        coordinatePartList.Add(coordinatePointList);
                    }
                    singleFeatureDict["geometry"]["coordinates"] = coordinatePartList;
                }
                Dictionary<string, dynamic> propertyDict = new Dictionary<string, dynamic>();
                singleFeatureDict["properties"] = propertyDict;
                for (int j = 0; j < geojsonPropertyValueList.Count; j++)
                {
                    if ((geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt16) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt32) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dInt64))
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToInt64(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                    else if ((geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dDouble) || (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dSingle))
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToDouble(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                    else if (geojsonPropertyValueList[j] == MyMapObjects.moValueTypeConstant.dText)
                    {
                        propertyDict[geojsonPropertyNameList[j]] = Convert.ToString(layer.Features.GetItem(i).Attributes.GetItem(j));
                    }
                }
            }
            string json = JsonConvert.SerializeObject(geojsonDict);
            return json;
        }

        #endregion
    }
}
