using System;
using System.IO;

namespace Sundown.App
{
	class BBCodeRenderer : CustomRenderer
	{
		protected override void Paragraph (Buffer ob, Buffer text)
		{
			ob.Put("\n\n");
			ob.Put(text);
		}
		protected override void Header(Buffer ob, Buffer text, int level)
		{
			ob.Put("\n\n\n[size={0}pt]{1}[/size]", 12, text);
		}

		protected override bool DoubleEmphasis(Buffer ob, Buffer text)
		{
			ob.Put("[b]{0}[/b]", text);
			return true;
		}

		protected override bool Emphasis (Buffer ob, Buffer text)
		{
			ob.Put("[u]{0}[/u]", text);
			return true;
		}

		protected override bool Link(Buffer ob, Buffer link, Buffer title, Buffer content)
		{
			ob.Put("[url={0}]{1}[/url]", link, content);
			return true;
		}

		protected override void List(Buffer ob, Buffer text, int flags)
		{
			ob.Put("\n[list type=decimal]\n");
			ob.Put(text);
			ob.Put("[/list]");
		}

		protected override void ListItem (Buffer ob, Buffer text, int flags)
		{
			ob.Put("[li]{0}[/li]\n", text);
		}

		protected override void BlockCode(Buffer ob, Buffer text, Buffer language)
		{
			ob.Put("\n[quote author={0}]{1}[/quote]\n", language, text);
		}

		static void Method3(string[] args)
		{
			if (args.Length < 1) {
				Console.WriteLine("You have to provide a file as an argument");
				return;
			}

			string inputfile = args[0];

			var md = new Markdown(new BBCodeRenderer(), new MarkdownExtensions() { FencedCode = true });

			using (Buffer buffer = new Buffer())
			try {
				using (var sr = new StreamReader(File.OpenRead(inputfile)))
				md.Render(buffer, sr.ReadToEnd());
			} catch (Exception exception) {
				Console.WriteLine("Unable to open input file {0: {1}", inputfile, exception.Message);
				return;
			} finally {
				Console.WriteLine(buffer);
			}
		}
	}

}
