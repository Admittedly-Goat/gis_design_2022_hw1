using System;
using System.Collections.Generic;

namespace MyMapObjects
{
    /// <summary>
    /// 字段集合类型
    /// </summary>
    public class moFields
    {
        #region 字段

        private readonly List<moField> _Fields;  //字段集合

        #endregion

        #region 构造函数

        public moFields()
        {
            _Fields = new List<moField>();
        }
        #endregion

        #region 属性

        /// <summary>
        /// 获取字段数目
        /// </summary>
        public int Count => _Fields.Count;

        /// <summary>
        /// 获取或设置主字段
        /// </summary>
        public string PrimaryField { get; set; } = "";

        /// <summary>
        /// 指示是否显示别名
        /// </summary>
        public bool ShowAlias { get; set; } = false;

        #endregion

        #region 方法

        /// <summary>
        /// 查找指定名称的字段，并返回其索引号，如无则返回-1
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public int FindField(string name)
        {
            int sFieldCount = _Fields.Count;
            for (int i = 0; i <= sFieldCount - 1; i++)
            {
                if (_Fields[i].Name.ToLower() == name.ToLower())
                {
                    return i;
                }
            }
            return -1;
        }

        /// <summary>
        /// 获取指定索引号的元素
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public moField GetItem(int index)
        {
            return _Fields[index];
        }

        /// <summary>
        /// 获取指定名称的字段
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public moField GetItem(string name)
        {
            int sIndex = FindField(name);
            return sIndex >= 0 ? _Fields[sIndex] : null;
        }

        //设置字段
        public void SetField(int index, moField field)
        {
            _Fields[index] = field;
        }

        /// <summary>
        /// 追加一个字段
        /// </summary>
        /// <param name="field"></param>
        public void Append(moField field)
        {
            if (FindField(field.Name) >= 0)
            {
                string sMessage = MyMapObjects.Properties
                    .Resources.String001;
                throw new Exception(sMessage);
            }
            _Fields.Add(field);
            //触发事件
            FieldAppended?.Invoke(this, field);
        }

        /// <summary>
        /// 删除指定索引号的字段
        /// </summary>
        /// <param name="index"></param>
        public void RemoveAt(int index)
        {
            moField sField = _Fields[index];
            _Fields.RemoveAt(index);
            //触发事件
            FieldRemoved?.Invoke(this, index, sField);
        }
        #endregion

        #region 事件

        internal delegate void FieldRemovedHandle
            (object sender, int fieldIndex, moField fieldRemoved);
        /// <summary>
        /// 有字段被删除了
        /// </summary>
        internal event FieldRemovedHandle FieldRemoved;

        internal delegate void FieldAppendedHandle
            (object sender, moField fieldAppended);
        /// <summary>
        /// 有字段被加入
        /// </summary>
        internal event FieldAppendedHandle FieldAppended;

        #endregion

        #region 私有函数


        #endregion
    }
}
