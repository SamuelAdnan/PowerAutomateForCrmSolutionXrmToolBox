﻿using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace PowerAutomateForCrmSolution
{
	// Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
	// To generate Base64 string for Images below, you can use https://www.base64-image.de/
	[Export(typeof(IXrmToolBoxPlugin)),
		ExportMetadata("Name", "PowerAutomateCloudForCrmSolution"),
		ExportMetadata("Description", "Provide ability to create a new crm cloud PowerAutomate workflow using common data service connectors and add it to selected crm solution. Works with OAuth or Certifcate type connections only."),
		// Please specify the base64 content of a 32x32 pixels image
		ExportMetadata("SmallImageBase64", "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"),
		// Please specify the base64 content of a 80x80 pixels image
		ExportMetadata("BigImageBase64", "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"),
		ExportMetadata("BackgroundColor", "Lavender"),
		ExportMetadata("PrimaryFontColor", "Black"),
		ExportMetadata("SecondaryFontColor", "Gray")]
	public class MyPlugin : PluginBase
	{
		public override IXrmToolBoxPluginControl GetControl()
		{
			return new MyPluginControl();
		}

		/// <summary>
		/// Constructor 
		/// </summary>
		public MyPlugin()
		{
			// If you have external assemblies that you need to load, uncomment the following to 
			// hook into the event that will fire when an Assembly fails to resolve
			// AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
		}

		/// <summary>
		/// Event fired by CLR when an assembly reference fails to load
		/// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
		/// For example, a folder named Sample.XrmToolBox.MyPlugin 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="args"></param>
		/// <returns></returns>
		private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
		{
			Assembly loadAssembly = null;
			Assembly currAssembly = Assembly.GetExecutingAssembly();

			// base name of the assembly that failed to resolve
			var argName = args.Name.Substring(0, args.Name.IndexOf(","));

			// check to see if the failing assembly is one that we reference.
			List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
			var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

			// if the current unresolved assembly is referenced by our plugin, attempt to load
			if (refAssembly != null)
			{
				// load from the path to this plugin assembly, not host executable
				string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
				string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
				dir = Path.Combine(dir, folder);

				var assmbPath = Path.Combine(dir, $"{argName}.dll");

				if (File.Exists(assmbPath))
				{
					loadAssembly = Assembly.LoadFrom(assmbPath);
				}
				else
				{
					throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
				}
			}

			return loadAssembly;
		}
	}
}