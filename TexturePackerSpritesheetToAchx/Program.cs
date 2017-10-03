using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TexturePackerSpritesheetToAchx
{
    class Program
    {
        static void Main(string[] args)
        {
            XDocument TexturePackerXml = XDocument.Load("test.xml");

            XElement frameListParent = TexturePackerXml.Root.Element("AnimationChain");

            foreach (var frame in frameListParent.Elements("Frame"))
            {
                var xNode = frame.Element("X");
                var yNode = frame.Element("Y");
                var widthNode = frame.Element("Width");
                var heightNode = frame.Element("Height");

                int x = int.Parse(xNode.Value);
                int y = int.Parse(yNode.Value);
                int width =  int.Parse(widthNode.Value);
                int height = int.Parse(heightNode.Value);

                frame.Add(new XElement("LeftCoordinate", x));
                frame.Add(new XElement("RightCoordinate", x + width));
                frame.Add(new XElement("TopCoordinate", y));
                frame.Add(new XElement("BottomCoordinate", y + height));
                

                xNode.Remove();
                yNode.Remove();
                widthNode.Remove();
                heightNode.Remove();
            }

            TexturePackerXml.Save("output.xml");
        }
    }
}
