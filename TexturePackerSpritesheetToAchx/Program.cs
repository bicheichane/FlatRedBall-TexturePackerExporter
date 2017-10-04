using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TexturePackerSpritesheetToAchx
{
    class Program
    {
        static void Main(string[] args)
        {
            IDictionary<string, SortedDictionary<int, XElement> > animationsDictionary = new Dictionary<string, SortedDictionary<int, XElement>>();
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

                string spriteName = sprite.Attribute("n").Value;
                string animationFrameId = Regex.Match(spriteName, @"\d+$").Value;

                XElement frame = new XElement("Frame",
                    new XElement("TextureName", InputFile.Root.Attribute("imagePath").Value),
                    new XElement("LeftCoordinate", x),
                    new XElement("RightCoordinate", x + width),
                    new XElement("TopCoordinate", y),
                    new XElement("BottomCoordinate", y + height));

                if (animationFrameId == "") //if sprite is not part of an animation, just do standalone animationChain for it
                {
                    XElement animationChain = new XElement("AnimationChain",
                        new XElement("Name", spriteName),
                        frame);
                    OutputFile.Add(animationChain);
                }
                else
                {
                    string animationName = spriteName.Replace(animationFrameId, "");

                    if(animationsDictionary.ContainsKey(animationName) == false)
                        animationsDictionary.Add(animationName, new SortedDictionary<int, XElement>());

                    animationsDictionary[animationName].Add(Int32.Parse(animationFrameId), frame);
                }

                
            }

            foreach (var animationChainDictionary in animationsDictionary)
            {
                XElement animationChain = new XElement("AnimationChain",
                    new XElement("Name", animationChainDictionary.Key));

                foreach (var animationFrame in animationChainDictionary.Value)
                {
                    animationChain.Add(animationFrame.Value);
                }

                OutputFile.Add(animationChain);
            }

            OutputFile.Save(output);
        }
    }
}
