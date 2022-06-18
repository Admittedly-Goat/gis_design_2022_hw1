using System.Drawing;
using System.Security.Cryptography;

namespace MyMapObjects
{
    public class moSimpleLineSymbol : moSymbol
    {
        #region 字段


        #endregion

        #region 构造函数

        public moSimpleLineSymbol()
        {
            CreateRandomColor();        //生成随机颜色
        }

        public moSimpleLineSymbol(string label)
        {
            Label = label;
            CreateRandomColor();        //生成随机颜色
        }

        #endregion

        #region 属性

        /// <summary>
        /// 获取符号类型
        /// </summary>
        public override moSymbolTypeConstant SymbolType => moSymbolTypeConstant.SimpleLineSymbol;

        /// <summary>
        /// 获取或设置标签
        /// </summary>
        public string Label { get; set; } = "";

        /// <summary>
        /// 指示是否可见
        /// </summary>
        public bool Visible { get; set; } = true;

        /// <summary>
        /// 获取或设置形状
        /// </summary>
        public moSimpleLineSymbolStyleConstant Style { get; set; } = moSimpleLineSymbolStyleConstant.Solid;

        /// <summary>
        /// 获取或设置颜色
        /// </summary>
        public Color Color { get; set; } = Color.LightPink;

        #endregion

        /// <summary>
        /// 获取或设置宽度
        /// </summary>
        public double Size { get; set; } = 0.35;

        #region 方法

        /// <summary>
        /// 复制
        /// </summary>
        public override moSymbol Clone()
        {
            moSimpleLineSymbol sSymbol = new moSimpleLineSymbol
            {
                Label = Label,
                Visible = Visible,
                Style = Style,
                Color = Color,
                Size = Size
            };
            return sSymbol;
        }
        public moSimpleLineSymbol Clone1()
        {
            moSimpleLineSymbol sSymbol = new moSimpleLineSymbol
            {
                Label = Label,
                Visible = Visible,
                Style = Style,
                Color = Color,
                Size = Size
            };
            return sSymbol;
        }

        #endregion

        #region 私有函数

        //生成随机颜色
        private void CreateRandomColor()
        {
            //总体思想：每个随机颜色RGB中总有一个为252，其他两个值的取值范围为179-245，这样取值的目的在于让地图颜色偏浅，美观
            //生成4个元素的字节数组，第一个值决定哪个通道取252，另外三个中的两个值决定另外两个通道的值
            byte[] sBytes = new byte[4];
            RNGCryptoServiceProvider sChanelRng = new RNGCryptoServiceProvider();
            sChanelRng.GetBytes(sBytes);
            int sChanelValue = sBytes[0];
            byte A = 255, R, G, B;
            if (sChanelValue <= 85)
            {
                R = 252;
                G = (byte)(179 + (66 * sBytes[2] / 255));
                B = (byte)(179 + (66 * sBytes[3] / 255));
            }
            else if (sChanelValue <= 170)
            {
                G = 252;
                R = (byte)(179 + (66 * sBytes[1] / 255));
                B = (byte)(179 + (66 * sBytes[3] / 255));
            }
            else
            {
                B = 252;
                R = (byte)(179 + (66 * sBytes[1] / 255));
                G = (byte)(179 + (66 * sBytes[2] / 255));
            }
            Color = Color.FromArgb(A, R, G, B);
        }
        #endregion
    }
}
