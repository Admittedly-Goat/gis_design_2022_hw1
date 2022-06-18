using System;
using System.Collections.Generic;

namespace MyMapObjects
{
    /// <summary>
    /// 唯一值渲染
    /// </summary>
    public class moUniqueValueRenderer : moRenderer
    {
        #region 字段

        private string _HeadTitle = ""; //在图层显示控件中的标题
        private bool _ShowHead = true; //在图层显示控件中是否显示标题
        private bool _ShowDefaultSymbol = false;
        //在图层显示控件中是否显示默认符号 

        #endregion

        #region 构造函数

        public moUniqueValueRenderer()
        { }
        #endregion

        #region 属性

        /// <summary>
        /// 获取渲染类型
        /// </summary>
        public override moRendererTypeConstant RendererType => moRendererTypeConstant.UniqueValue;

        /// <summary>
        /// 获取或设置绑定字段
        /// </summary>
        public string Field { get; set; } = "";

        /// <summary>
        /// 获取唯一值数目
        /// </summary>
        public int ValueCount => Values.Count;

        /// <summary>
        /// 获取或设置默认符号
        /// </summary>
        public moSymbol DefaultSymbol { get; set; }
        //其他属性不再编写,自行添加
        public List<moSymbol> Symbols { get; set; } = new List<moSymbol>();

        public List<string> Values { get; set; } = new List<string>();
        #endregion

        #region 方法

        /// <summary>
        /// 获取指定索引号的唯一值
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string GetValue(int index)
        {
            return Values[index];
        }

        /// <summary>
        /// 设置指定索引号的唯一值
        /// </summary>
        /// <param name="index"></param>
        /// <param name="value"></param>
        public void SetValue(int index, string value)
        {
            Values[index] = value;
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
        /// <param name="symbol"></param>
        public void SetSymbol(int index, moSymbol symbol)
        {
            Symbols[index] = symbol;
        }
        /// <summary>
        /// 增加一个唯一值及对应符号
        /// </summary>
        /// <param name="value"></param>
        /// <param name="symbol"></param>
        public void AddUniqueValue(string value, moSymbol symbol)
        {
            Values.Add(value);
            Symbols.Add(symbol);
        }

        /// <summary>
        /// 增加唯一值数组和对应的符号数组
        /// </summary>
        /// <param name="values"></param>
        /// <param name="symbols"></param>
        public void AddUniqueValues(string[] values,
            moSymbol[] symbols)
        {
            if (values.Length != symbols.Length)
            {
                throw new Exception
                    ("the length of the two arrays is not equal!");
            }
            Values.AddRange(values);
            Symbols.AddRange(symbols);
        }

        /// <summary>
        /// 根据指定唯一值获取对应的符号，如果该值不存在则返回默认符号
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public moSymbol FindSymbol(string value)
        {
            int sValueCount = Values.Count;
            for (int i = 0; i <= sValueCount - 1; i++)
            {
                if (Values[i] == value)
                {
                    return Symbols[i];
                }
            }
            return DefaultSymbol;
        }

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public override moRenderer Clone()
        {
            moUniqueValueRenderer sRenderer = new moUniqueValueRenderer
            {
                Field = Field,
                _HeadTitle = _HeadTitle,
                _ShowHead = _ShowHead
            };
            int sValueCount = Values.Count;
            for (int i = 0; i <= sValueCount - 1; i++)
            {
                string sValue = Values[i];
                moSymbol sSymbol = null;
                if (Symbols[i] != null)
                {
                    sSymbol = Symbols[i].Clone();
                }

                sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (DefaultSymbol != null)
            {
                sRenderer.DefaultSymbol = DefaultSymbol.Clone();
            }

            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;

        }
        public moUniqueValueRenderer Clone1()
        {
            moUniqueValueRenderer sRenderer = new moUniqueValueRenderer
            {
                Field = Field,
                _HeadTitle = _HeadTitle,
                _ShowHead = _ShowHead
            };
            int sValueCount = Values.Count;
            for (int i = 0; i <= sValueCount - 1; i++)
            {
                string sValue = Values[i];
                moSymbol sSymbol = null;
                if (Symbols[i] != null)
                {
                    sSymbol = Symbols[i].Clone();
                }

                sRenderer.AddUniqueValue(sValue, sSymbol);
            }
            if (DefaultSymbol != null)
            {
                sRenderer.DefaultSymbol = DefaultSymbol.Clone();
            }

            sRenderer._ShowDefaultSymbol = _ShowDefaultSymbol;
            return sRenderer;

        }

        #endregion


    }
}
