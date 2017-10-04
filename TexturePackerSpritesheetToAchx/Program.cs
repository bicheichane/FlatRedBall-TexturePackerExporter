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

            XDocument InputFile = XDocument.Load(@input);

            //XNamespace xsi = @"http://www.w3.org/2001/XMLSchema-instance", xsd = @"http://www.w3.org/2001/XMLSchema";

            XElement OutputFile =
                new XElement("AnimationChainArraySave",
                    //new XAttribute(xsi + "xsi", xsi.NamespaceName),
                    //new XAttribute(xsd + "xsd", xsd.NamespaceName),
                    new XElement("FileRelativeTextures", true),
                    new XElement("TimeMeasurementUnit", "Undefined"),
                    new XElement("CoordinateType", "Pixel"));

            foreach (var sprite in InputFile.Root.Elements("sprite"))
            {
                var xNode = sprite.Attribute("x");
                var yNode = sprite.Attribute("y");
                var widthNode = sprite.Attribute("w");
                var heightNode = sprite.Attribute("h");

                int x = int.Parse(xNode.Value);
                int y = int.Parse(yNode.Value);
                int width =  int.Parse(widthNode.Value);
                int height = int.Parse(heightNode.Value);

                
                XElement frame = new XElement("Frame", 
                                    new XElement("TextureName", InputFile.Root.Attribute("imagePath").Value),
                                    new XElement("LeftCoordinate", x),
                                    new XElement("RightCoordinate", x + width),
                                    new XElement("TopCoordinate", y),
                                    new XElement("BottomCoordinate", y + height));

                XElement animationChain = new XElement("AnimationChain",
                                            new XElement("Name", sprite.Attribute("n").Value),
                                            frame);
                OutputFile.Add(animationChain);
            }

            OutputFile.Save(output);
        }
    }
}
