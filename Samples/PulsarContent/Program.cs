using System;
using Pulsar;
using Pulsar.Host;
using Pulsar.Services;
using Pulsar.Services.Implements;
using Pulsar.Services.Implements.Content;
using Pulsar.Services.Implements.Graphics;

namespace PulsarContent
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			new Game()
				.AddHeuristic<DefaultPulsarInjection>()
				.AddService<ISpriteBatchService, SpriteBatchService>()
				.AddService<IShapeBatchService, ShapeBatchService>()
				.AddService<ITextBatchService, TextBatchService>()
				.AddService<IContentService, ContentService>()
				.AddService<IWindowService, WindowService>()
				.AddModule<ModuleTest>()
				.Run();
		}
	}
}
