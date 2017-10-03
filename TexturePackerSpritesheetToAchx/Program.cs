using System;
using System.Collections.Generic;
using System.IO;
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
            string input, output;

            if (args.Length == 0)
            {
                input = "test.xml";
                output = "output.xml";
            }
            else if (args.Length != 2)
                throw new IOException("Proper argument usage is <input file location> <output file location>");
            else
            {
                input = args[0];
                output = args[1];
            }

            XDocument TexturePackerXml = XDocument.Load(@input);

            foreach (var animationChain in TexturePackerXml.Root.Elements("AnimationChain"))
            {
                var frame = animationChain.Element("Frame");

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

            TexturePackerXml.Save(output);
        }
    }
}
