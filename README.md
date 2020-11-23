# ImgDemo
1.替换图片背景色
使用C\#实现对图片内某种颜色的替换

2020年11月18日

9:30

**背景:**

写这个程序的起因是前段时间接了个私活,要求用winform做一个给图片批量打水印的程序,大概如下这种:

![](media/f9637159ccdd142b69b246cbd1a36c29.jpeg)

>   计算机生成了可选文字: 工记录 施工区域： 备注信息：

写完后和另一个朋友聊天时聊到这方面,他问我能画图那能不能对图片中颜色做替换,比如给证件照换个背景色什么的,后面我也就抱着试试看的心态做了一下.话不多说,程序如下.

**先看看demo的样子:**

![](media/574700af4ba9c7a823a7ae4a8e876818.gif)

>   很简单的一个demo,主要实现的功能就是载入图片,选择要替换的颜色(默认查

找的是左上角坐标原点的颜色,要替换别的颜色只需要用鼠标在那部分单机并点击查找背景色),选择替换色,替换颜色和保存的功能.

**效果图:**

图片处理

![](media/73610980d2f543a2d4466869294aeba3.png)

![](media/3a2dd43147ea80137ed219f3b4794b9c.png)

![](media/1dcfd8caf7d4b4cb5abb84149e2c7885.png)

(原图) (处理后1) (处理后2)

证件照换背景色

![](media/6dd39e3f6f6ff8e0eb1aa2a7191f64d8.png)

![](media/4b2dcfad67ee2537a7da76793ee10ba5.png)

(原图) (处理后1)

**程序很简单,大体结构如下:**

![计算机生成了可选文字: 交互事件 \#region 1个引用《0项更改《0名作者，0项更改 sender，EventArgse)[二二囗 privatevoidTextLostFocus(object 1个引用《0项更改《0名作者，0项更改 privatevoidbtnSe1ectImg_C1ick(0bjectsender,EventArgse)[二二囗 1个引用《0项更改《0名作者，0项更改 sender，EventArgse)[二二囗 privatevoidbtnQuerybgC010r_C1ick(object 1个引用《0项更改《0名作者，0项更改 EventArgse)[二二囗 privatevoidbtnSe1ectRep1acementC010rClick(object sender 1个引用《0项更改《0名作者，0项更改 sender,EventArgse)[二二囗 privatevoidbtnRep1ace_C1ick()bject 1个引用《0项更改《0名作者，0项更改 sender，EventArgse)[二二囗 privatevoidbtnSave_C1ick(object 1个引用《0项更改《0名作者，0项更改 sender，MouseEventArgse)[二二囗 privatevoidpbShowPane1_MouseC1ick(object \#endregion](media/4c792cd86f3310351f976861ae577feb.png)

![](media/bb8db474e43215ab5280871a7dbca285.png)

>   计算机生成了可选文字: ///〈summary\>Image图像转化为32位位图 3》用0项更0
>   ，0项更 publicBitmapConvertT032bpp(Imageimg)[〔〔囗
>   图片指定颜色替换成另一种颜色Conver\_ 3()itmap \#region r，intg,intb)
>   ///〈summary\>指定颜色替换成另一种颜色 1》用0项更0 ，0项更 publicBitmapimg，
>   inth， Int \#endregion Img， Int Int R， W， inth,intR， Int intG，intB，
>   Int r, Int Int

**核心代码如下:**

>   /// \<summary\>

>   /// 指定颜色替换成另一种颜色

>   /// \</summary\>

>   /// \<param name="img"\>原图\</param\>

>   /// \<param name="w"\>图宽\</param\>

>   /// \<param name="h"\>图高\</param\>

>   /// \<param name="R"\>要被替换颜色的RGB的R\</param\>

>   /// \<param name="G"\>要被替换颜色的RGB的G\</param\>

>   /// \<param name="B"\>要被替换颜色的RGB的B\</param\>

>   /// \<param name="r"\>替换色的RGB的R\</param\>

>   /// \<param name="g"\>替换色的RGB的G\</param\>

>   /// \<param name="b"\>替换色的RGB的B\</param\>

>   /// \<returns\>处理后的结果图像\</returns\>

>   public Bitmap ReplaceColor(Bitmap img, int w, int h, int R, int G, int B,
>   int r, int g, int b)

>   {

>   Bitmap bt = new Bitmap(ConvertTo32bpp(img));

>   Rectangle rect = new Rectangle(0, 0, w, h);

>   BitmapData bmpdata = bt.LockBits(rect, ImageLockMode.ReadWrite,
>   bt.PixelFormat);

>   IntPtr ptr = bmpdata.Scan0;

>   int bytes = Math.Abs(bmpdata.Stride) \* bt.Height;

>   byte[] rgbValues = new byte[bytes];

>   System.Runtime.InteropServices.Marshal.Copy(ptr, rgbValues, 0, bytes);

>   int len = rgbValues.Length;

>   byte a = (byte)int.Parse(tbTolerance.Text);

>   byte R1 = (byte)R;

>   byte G1 = (byte)G;

>   byte B1 = (byte)B;

>   byte r1 = (byte)r;

>   byte g1 = (byte)g;

>   byte b1 = (byte)b;

>   for (int i = 0; i \< len; i += 4)

>   {

>   //Format32bppRgb是用4个字节表示一个像素,第一个字节表示RGB的B值,第一个表示为G值,第三个表示为R值,第四个表示为Alpha值

>   if (Math.Abs(rgbValues[i] - B1) \< a && Math.Abs(rgbValues[i + 1] - G1) \< a
>   && Math.Abs(rgbValues[i + 2] - R1) \< a)

>   {

>   rgbValues[i] = b1;

>   rgbValues[i + 1] = g1;

>   rgbValues[i + 2] = r1;

>   }

>   }

>   System.Runtime.InteropServices.Marshal.Copy(rgbValues, 0, ptr, bytes);

>   bt.UnlockBits(bmpdata);

>   return bt;

>   }

**代码说明:**

>   通过LockBits方法来锁定系统内存中现有的bitmap位图,使其可以用编程

的方式进行更改.然后通过用bitmapdata的Scan0属性来找到位图第一个像素数据的位置,进而通过bitmapdata的Stride属性来得到位图的扫描宽度(和图片的width属性不一样,Stride是内存中实际位图每行的宽度,存在一个补齐为4的倍数).然后通过宽度和高度的乘积得到位图在内存中占有的字节(byte)数组大小,进而用Marshal.Copy方法从内存中得到这些位图的像素数据,然后采用for循环去遍历每一个像素(4字节,顺序是bgrAlpha)上的颜色数值和要替换的颜色数值的差的绝对值是否在设定的容差范围内,如果在就用替换的颜色数值去覆盖原有颜色数值.

**程序地址:**

>   <https://github.com/JingChao94/ImgDemo>

**参考资料:**

>   <https://docs.microsoft.com/zh-cn/dotnet/api/system.drawing.imaging.bitmapdata?view=dotnet-plat-ext-5.0>

>   <https://docs.microsoft.com/zh-cn/dotnet/api/system.drawing.bitmap?view=dotnet-plat-ext-5.0>

>   <https://blog.csdn.net/qq_42170268/article/details/86573796>

**作者介绍:**

木石,菜鸟软件工程师.会一点cs和bs程序开发,常用C\#,偶尔也改改
python脚本写写js之类的,目前在一家自动化公司任职,才开始接触视觉检测已经伺服电机梯形图之类的,希望可以保持进步,持续成长下去.
