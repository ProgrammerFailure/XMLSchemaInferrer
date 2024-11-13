using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;

namespace XmlSchemaInferenceApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Specify the folder containing XML files
            string folderPath = "D:\\GamesAndCo\\CustomGameFiles\\ProgrammerFailure\\KSPDocsHubFolder\\xml\\xml";  // Update this path
            string outputSchemaPath = "outputSchema.xsd";

            // Initialize XmlSchemaInference and XmlSchemaSet for inferring and merging schemas
            XmlSchemaInference schemaInference = new XmlSchemaInference();
            XmlSchemaSet schemaSet = new XmlSchemaSet();

            // Process each XML file in the folder
            foreach (var filePath in Directory.GetFiles(folderPath, "*.xml"))
            {
                try
                {
                    // Read each XML file
                    using (XmlReader reader = XmlReader.Create(filePath))
                    {
                        // Infer the schema for the XML file
                        XmlSchemaSet inferredSchema = schemaInference.InferSchema(reader);

                        // Merge the inferred schema into the main schema set
                        schemaSet.Add(inferredSchema);
                    }
                    Console.WriteLine($"Processed {filePath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error processing {filePath}: {ex.Message}");
                }
            }

            // Write the inferred schema to an output XSD file
            using (FileStream fs = new FileStream(outputSchemaPath, FileMode.Create))
            {
                foreach (XmlSchema schema in schemaSet.Schemas())
                {
                    schema.Write(fs);
                }
            }

            Console.WriteLine($"Schema saved to {outputSchemaPath}");
        }
    }
}
