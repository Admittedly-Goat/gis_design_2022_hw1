namespace MyMapObjects
{
    /// <summary>
    /// 字段
    /// </summary>
    public class moField
    {
        #region 字段


        #endregion

        #region 构造函数

        public moField(string name)
        {
            Name = name;
            AliasName = name;
        }

        public moField(string name, moValueTypeConstant valueType)
        {
            Name = name;
            AliasName = name;
            ValueType = valueType;
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取字段名称
        /// </summary>
        public string Name { get; set; } = "";

        /// <summary>
        /// 获取或设置字段别名
        /// </summary>
        public string AliasName { get; set; } = "";

        /// <summary>
        /// 获取值类型
        /// </summary>
        public moValueTypeConstant ValueType { get; } = moValueTypeConstant.dInt32;

        /// <summary>
        /// 获取字段长度
        /// </summary>
        public int Length { get; private set; }

        #endregion

        #region 方法

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public moField Clone()
        {
            moField sField = new moField(Name, ValueType)
            {
                AliasName = AliasName,
                Length = Length
            };
            return sField;
        }
        #endregion
    }
}
