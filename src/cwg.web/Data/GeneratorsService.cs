using System;
using System.Linq;
using cwg.web.Enums;
using cwg.web.Generators;
using cwg.web.Repositories;

namespace cwg.web.Data
{
    public class GeneratorsService
    {
        private BaseGenerator GetGenerator(string name) => GeneratorRepository.GetGenerators().FirstOrDefault(a => a.Name == name);

        public GenerationResponseModel GenerateFile(GenerationRequestModel model)
        {
            try
            {
                var generator = GetGenerator(model.FileType);

                if (generator == null)
                {
                    throw new Exception($"{model.FileType} was not found");
                }

                var (sha1, fileName) = generator.GenerateFiles(model.NumberToGenerate, (ThreatLevels)Enum.Parse(typeof(ThreatLevels), model.ThreatLevel), model.Injection);

                return new GenerationResponseModel
                {
                    FileName = fileName,
                    SHA1 = sha1,
                    FileType = model.FileType,
                    ThreatLevel = model.ThreatLevel
                };
            } catch (Exception ex)
            {
                NLog.LogManager.GetCurrentClassLogger().Error($"Failed to GenerateFile ({model.FileType}): {ex}");

                return null;
            }
        }
    }
}