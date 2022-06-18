using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace MyMapObjects
{
    /// <summary>
    /// 分级渲染
    /// </summary>
    public class moClassBreaksRenderer : moRenderer
    {
        #region 字段
        private string _Field = ""; //绑定字段
        private string _HeadTitle = "";     //在图层显示控件中的标题
        private bool _ShowHead = true;      //在图层显示控件是否显示标题
        private bool _ShowDefaultSymbol = true; //在图层显示控件中是否显示默认符号

        #endregion

        #region 构造函数
        public moClassBreaksRenderer()
        { }

        #endregion

        #region 属性

        /// <summary>
        /// 获取渲染类型
        /// </summary>
        public override moRendererTypeConstant RendererType => moRendererTypeConstant.ClassBreaks;


        /// <summary>
        /// 获取或设置绑定字段名称
        /// </summary>
        public string Field
        {
            get => _Field;
            set
            {
                _Field = value;
                _HeadTitle = value;
            }
        }

        /// <summary>
        /// 获取分割值数目
        /// </summary>
        public int BreakCount => BreakValues.Count;

        /// <summary>
        /// 获取或设置默认符号
        /// </summary>
        public moSymbol DefaultSymbol { get; set; }

        public List<moSymbol> Symbols { get; set; } = new List<moSymbol>();
        public List<double> BreakValues { get; set; } = new List<double>();

        #endregion

        #region 方法

        /// <summary>
        /// 获取指定索引号的分割值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public double GetBreakValue(int index)
        {
            return BreakValues[index];
        }

        /// <summary>
        /// 设置指定索引号的分割值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetBreakValue(int index, double breakValue)
        {
            BreakValues[index] = breakValue;
        }

        /// <summary>
        /// 获取指定索引号的符号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moSymbol GetSymbol(int index)
        {
            return Symbols[index];
        }

        /// <summary>
        /// 设置指定索引号的符号
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetSymbol(int index, moSymbol symbol)
        {
            Symbols[index] = symbol;
        }

        /// <summary>
        /// 增加一个分割值及对应的符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbol"></param>
        public void AddBreakValue(double value, moSymbol symbol)
        {
            BreakValues.Add(value);
            Symbols.Add(symbol);
        }

        /// <summary>
        /// 增加分割值数组以及对应的符号数组
        /// </summary>
        /// <param name="values"></param>
        /// <param name="symbols"></param>
        public void AddBreakValues(double[] values, moSymbol[] symbols)
        {
            if (values.Length != symbols.Length)
            {
                throw new Exception("the length of the two arrays is not equal!");
            }
            BreakValues.AddRange(values);
            Symbols.AddRange(symbols);
        }

        /// <summary>
        /// 根据指定值获取对应的符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public moSymbol FindSymbol(double value)
        {
            int sBreakCount = BreakValues.Count;
            if (sBreakCount == 0)
            {
                return DefaultSymbol;
            }

            if (value < BreakValues[0])    //小于第一个分割值
            {
                return Symbols[0];
            }
            else
            {
                for (int i = 0; i <= sBreakCount - 2; i++)
                {
                    if (i < sBreakCount - 2 && value >= BreakValues[i] && value < BreakValues[i + 1])
                    {
                        return Symbols[i + 1];
                    }
                    else if (i == sBreakCount - 2 && value >= BreakValues[i] && value <= BreakValues[i + 1])
                    {
                        return Symbols[i + 1];
                    }
                }
            }
            return DefaultSymbol;
        }
        /// <summary>
        /// 为所有点、线符号赋予渐变尺寸
        /// </summary>
        /// <param name="size"></param>
        public void RampSize(double size)
        {
            int sBreakCount = BreakValues.Count;
            double[] Size = new double[sBreakCount];
            if (sBreakCount <= 0)
            {
                return;
            }
            else if (sBreakCount <= 4)
            {
                double minSize = size * 2 / sBreakCount;
                for (int i = 0; i < sBreakCount; i++)
                {
                    Size[i] = (i + 1) * minSize;
                }
            }
            else
            {
                double minSize = size / 4;
                for (int i = 0; i < sBreakCount; i++)
                {
                    Size[i] = (i + 1) * minSize;
                }
            }
            for (int i = 0; i < sBreakCount; i++)
            {
                if (Symbols[i] != null)
                {
                    if (Symbols[i].SymbolType == moSymbolTypeConstant.SimpleMarkerSymbol)
                    {
                        moSimpleMarkerSymbol sSymbol = (moSimpleMarkerSymbol)Symbols[i];
                        sSymbol.Size = Size[i];
                    }
                    else if (Symbols[i].SymbolType == moSymbolTypeConstant.SimpleLineSymbol)
                    {
                        moSimpleLineSymbol sSymbol = (moSimpleLineSymbol)Symbols[i];
                        sSymbol.Size = Size[i];
                    }
                }
            }


        }
        /// <summary>
        /// 为所有符号生成渐变色
        /// </summary>
        /// <param name="startColor"></param>
        /// <param name="endColor"></param>
        public void RampColor(Color startColor, Color endColor)
        {
            int sBreakCount = BreakValues.Count;
            if (sBreakCount <= 0)
            {
                return;
            }

            int A1 = startColor.A;
            _ = startColor.R;
            _ = startColor.G;
            _ = startColor.B;
            int A2 = endColor.A;
            _ = endColor.R;
            _ = endColor.G;
            _ = endColor.B;
            int A, R, G, B;
            double H, S, V;
            Color[] sColors = new Color[sBreakCount];
            if (sBreakCount == 1)
            {
                sColors[0] = startColor;
            }
            else
            {
                //将起始和终止颜色转换为HSV
                double[] sStartHSV = RGBToHSV(startColor.R, startColor.G, startColor.B);
                double[] sEndHSV = RGBToHSV(endColor.R, endColor.G, endColor.B);
                sColors[0] = startColor;
                sColors[sBreakCount - 1] = endColor;
                for (int i = 1; i <= sBreakCount - 2; i++)
                {
                    H = sStartHSV[0] + (i * (sEndHSV[0] - sStartHSV[0]) / sBreakCount);
                    S = sStartHSV[1] + (i * (sEndHSV[1] - sStartHSV[1]) / sBreakCount);
                    V = sStartHSV[2] + (i * (sEndHSV[2] - sStartHSV[2]) / sBreakCount);
                    byte[] sRGB = HSVToRGB(H, S, V);
                    A = A1 + (i * (A2 - A1) / sBreakCount);
                    R = sRGB[0];
                    G = sRGB[1];
                    B = sRGB[2];
                    sColors[i] = Color.FromArgb(A, R, G, B);
                }
            }
            for (int i = 0; i <= sBreakCount - 1; i++)
            {
                if (Symbols[i] != null)
                {
                    if (Symbols[i].SymbolType == moSymbolTypeConstant.SimpleMarkerSymbol)
                    {
                        moSimpleMarkerSymbol sSymbol = (moSimpleMarkerSymbol)Symbols[i];
                        sSymbol.Color = sColors[i];
                    }
                    else if (Symbols[i].SymbolType == moSymbolTypeConstant.SimpleLineSymbol)
                    {
                        moSimpleLineSymbol sSymbol = (moSimpleLineSymbol)Symbols[i];
                        sSymbol.Color = sColors[i];
                    }
                    else if (Symbols[i].SymbolType == moSymbolTypeConstant.SimpleFillSymbol)
                    {
                        moSimpleFillSymbol sSymbol = (moSimpleFillSymbol)Symbols[i];
                        sSymbol.Color = sColors[i];
                    }
                }
            }

        }

        /// <summary>
        /// 复制
        /// </summary>
        /// <returns></returns>
        public override moRenderer Clone()
        {
            moClassBreaksRenderer sRenderer = new moClassBreaksRenderer
            {
                _Field = _Field,
                _HeadTitle = _HeadTitle,
                _ShowHead = _ShowHead
            };

            int sBreakCount = BreakValues.Count;
            for (int i = 0; i <= sBreakCount - 1; i++)
            {
                _ = BreakValues[i];
                if (Symbols[i] != null)
                {
                    _ = Symbols[i].Clone();
                }
                //sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (DefaultSymbol != null)
            {
                sRenderer.DefaultSymbol = DefaultSymbol.Clone();
            }
            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;
        }
        public moClassBreaksRenderer Clone1()
        {
            moClassBreaksRenderer sRenderer = new moClassBreaksRenderer
            {
                _Field = _Field,
                _HeadTitle = _HeadTitle,
                _ShowHead = _ShowHead
            };

            int sBreakCount = BreakValues.Count;
            for (int i = 0; i <= sBreakCount - 1; i++)
            {
                _ = BreakValues[i];
                if (Symbols[i] != null)
                {
                    _ = Symbols[i].Clone();
                }
                //sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (DefaultSymbol != null)
            {
                sRenderer.DefaultSymbol = DefaultSymbol.Clone();
            }
            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;
        }
        #endregion

        #region 私有函数

        private double[] RGBToHSV(double R, double G, double B)
        {
            byte[] sRGB = new byte[3];
            sRGB[0] = (byte)R; sRGB[1] = (byte)G; sRGB[2] = (byte)B;
            double H, S, V;
            byte sMax = sRGB.Max();
            byte sMin = sRGB.Min();
            V = sMax;
            if (sMax != sMin)
            {
                S = (sMax - sMin) / sMax;
                H = R == sMax
                    ? (G - B) / (sMax - sMin) * 60
                    : G == sMax ? 120 + ((B - R) / (sMax - sMin) * 60) : 240 + ((R - G) / (sMax - sMin) * 60);

                if (H < 0)
                {
                    H += 360;
                }
            }
            else
            {
                S = 0;
                H = -1;
            }
            double[] sHSV = new double[3];
            sHSV[0] = H;
            sHSV[1] = S;
            sHSV[2] = V;
            return sHSV;
        }

        //将HSV转换为RGB
        private byte[] HSVToRGB(double H, double S, double V)
        {
            byte[] sRGB = new byte[3];
            byte R, G, B;
            if (S == 0)
            {
                R = (byte)V; G = (byte)V; B = (byte)V;
            }
            else
            {
                H /= 60;
                int i = (int)H;
                double f = H - i;
                double aa = V * (1 - S);
                double bb = V * (1 - (S * f));
                double cc = V * (1 - (S * (1 - f)));
                switch (i)
                {
                    case 0:
                        R = (byte)V;
                        G = (byte)cc;
                        B = (byte)aa;
                        break;
                    case 1:
                        R = (byte)bb;
                        G = (byte)V;
                        B = (byte)aa;
                        break;
                    case 2:
                        R = (byte)aa;
                        G = (byte)V;
                        B = (byte)cc;
                        break;
                    case 3:
                        R = (byte)aa;
                        G = (byte)bb;
                        B = (byte)V;
                        break;
                    case 4:
                        R = (byte)cc;
                        G = (byte)aa;
                        B = (byte)V;
                        break;
                    case 5:
                        R = (byte)V;
                        G = (byte)aa;
                        B = (byte)bb;
                        break;
                    default:
                        R = 0; G = 0; B = 0;
                        break;
                }
            }
            sRGB[0] = R;
            sRGB[1] = G;
            sRGB[2] = B;
            return sRGB;
        }
        #endregion
    }
}
