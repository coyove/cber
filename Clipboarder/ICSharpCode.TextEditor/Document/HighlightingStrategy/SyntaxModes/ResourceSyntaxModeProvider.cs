// <file>
//     <copyright see="prj:///doc/copyright.txt"/>
//     <license see="prj:///doc/license.txt"/>
//     <owner name="Mike Krüger" email="mike@icsharpcode.net"/>
//     <version>$Revision: 1965 $</version>
// </file>

using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Xml;

namespace ICSharpCode.TextEditor.Document
{
	public class ResourceSyntaxModeProvider : ISyntaxModeFileProvider
	{
		List<SyntaxMode> syntaxModes = null;
		
		public ICollection<SyntaxMode> SyntaxModes {
			get {
				return syntaxModes;
			}
		}
		
		public ResourceSyntaxModeProvider()
		{
			Assembly assembly = typeof(SyntaxMode).Assembly;
            //Stream syntaxModeStream = assembly.GetManifestResourceStream("ICSharpCode.TextEditor.Resources.SyntaxModes.xml");

            //if (syntaxModeStream != null) {
            syntaxModes = SyntaxMode.GetSyntaxModes(new StringReader(Clipboarder.Properties.Resources.SyntaxModes));
			//} else {
			//	syntaxModes = new List<SyntaxMode>();
			//}
		}
		
		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
            var s = new MemoryStream(Clipboarder.Properties.Resources.ResourceManager.GetObject(syntaxMode.FileName) as byte[]);
			return new XmlTextReader(s);
		}
		
		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}
