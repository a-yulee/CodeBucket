#pragma warning disable 1591
// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace CodeBucket.Views
{
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


[System.CodeDom.Compiler.GeneratedCodeAttribute("RazorTemplatePreprocessor", "2.6.0.0")]
public partial class SyntaxHighlighterView : SyntaxHighlighterViewBase
{

#line hidden

#line 1 "SyntaxHighlighterView.cshtml"
public SourceBrowserModel Model { get; set; }

#line default
#line hidden


public override void Execute()
{
WriteLiteral("<html>\n<head>\n<meta");

WriteLiteral(" name=\"viewport\"");

WriteAttribute ("content", " content=\"", "\""
, Tuple.Create<string,object,bool> ("", "minimum-scale=", true)

#line 4 "SyntaxHighlighterView.cshtml"
       , Tuple.Create<string,object,bool> ("", Model.Scale

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> (" ", "maximum-scale=4.0", true)
);
WriteLiteral(">\n<link");

WriteLiteral(" rel=\"stylesheet\"");

WriteAttribute ("href", " href=\"", "\""
, Tuple.Create<string,object,bool> ("", "WebResources/styles/", true)

#line 5 "SyntaxHighlighterView.cshtml"
           , Tuple.Create<string,object,bool> ("", Model.Theme

#line default
#line hidden
, false)
, Tuple.Create<string,object,bool> ("", ".css", true)
);
WriteLiteral(" />\n<style>\nhtml { height: 100%; width: 100%; }\nbody { \n    margin: 0; \n    min-h" +
"eight: 100%; \n    min-width: 100%;\n    text-size-adjust: 100%; \n    -webkit-text" +
"-size-adjust: none;\n    font-size: ");


#line 14 "SyntaxHighlighterView.cshtml"
           Write(Model.FontSize);


#line default
#line hidden
WriteLiteral(@"px;
}

body > pre { 
    margin: 0em; 
    min-width: 100%; 
    min-height: 100%; 
    overflow: none;
}

body > pre * {
    overflow: none;
}

.hljs { 
    width: 100%;
    min-width: 100%; min-height: 100%; 
    box-sizing: border-box; 
    overflow: none;
    padding: 0;
}

code {
  display: block;
  width: 100%;
  min-width: 100%;
  padding: 0;
}

.pre-numbering {
    padding: 0 2px 0 2px;
    border-right: 1px solid #C3CCD0;
    text-align: right;
    color: #AAA;
    background-color: #EEE;
    display: inline-block;
    float: left;
    margin-top: 0;
    margin-right: 15px;
}
</style>
</head>
<body>
    <pre><code");

WriteLiteral(" id=\"code\"");

WriteAttribute ("class", " class=\"", "\""

#line 57 "SyntaxHighlighterView.cshtml"
, Tuple.Create<string,object,bool> ("", Model.Language

#line default
#line hidden
, false)
);
WriteLiteral(">");


#line 57 "SyntaxHighlighterView.cshtml"
                                            Write(Model.Content);


#line default
#line hidden
WriteLiteral("</code></pre>\n    <script");

WriteLiteral(" src=\"http://code.jquery.com/jquery-2.1.3.min.js\"");

WriteLiteral("></script>\n    <script");

WriteLiteral(" src=\"WebResources/highlight.pack.js\"");

WriteLiteral(@"></script>
    <script>
        $(function() {
            hljs.configure({
              tabReplace: '    '
            });

            $('body').css('width', $(document).width() + 75 + 'px');

            hljs.initHighlighting();
            document.getElementById(""code"").style.display = 'block';

            $('#code').each(function(){
                var lines = $(this).text().split('\n').length - 1;
                var $numbering = $('<div/>').addClass('pre-numbering');
                $(this)
                    .addClass('has-numbering')
                    .prepend($numbering);
                for(i=1;i<=lines + 1;i++){
                    $numbering.append(document.createTextNode(i + '\n'));
                }
            });
        });
    </script>
</body>
</html> ");

}
}

// NOTE: this is the default generated helper class. You may choose to extract it to a separate file 
// in order to customize it or share it between multiple templates, and specify the template's base 
// class via the @inherits directive.
public abstract class SyntaxHighlighterViewBase
{

		// This field is OPTIONAL, but used by the default implementation of Generate, Write, WriteAttribute and WriteLiteral
		//
		System.IO.TextWriter __razor_writer;

		// This method is OPTIONAL
		//
		/// <summary>Executes the template and returns the output as a string.</summary>
		/// <returns>The template output.</returns>
		public string GenerateString ()
		{
			using (var sw = new System.IO.StringWriter ()) {
				Generate (sw);
				return sw.ToString ();
			}
		}

		// This method is OPTIONAL, you may choose to implement Write and WriteLiteral without use of __razor_writer
		// and provide another means of invoking Execute.
		//
		/// <summary>Executes the template, writing to the provided text writer.</summary>
		/// <param name="writer">The TextWriter to which to write the template output.</param>
		public void Generate (System.IO.TextWriter writer)
		{
			__razor_writer = writer;
			Execute ();
			__razor_writer = null;
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the template output without HTML escaping it.</summary>
		/// <param name="value">The literal value.</param>
		protected void WriteLiteral (string value)
		{
			__razor_writer.Write (value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes a literal value to the TextWriter without HTML escaping it.</summary>
		/// <param name="writer">The TextWriter to which to write the literal.</param>
		/// <param name="value">The literal value.</param>
		protected static void WriteLiteralTo (System.IO.TextWriter writer, string value)
		{
			writer.Write (value);
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>Writes a value to the template output, HTML escaping it if necessary.</summary>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected void Write (object value)
		{
			WriteTo (__razor_writer, value);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>Writes an object value to the TextWriter, HTML escaping it if necessary.</summary>
		/// <param name="writer">The TextWriter to which to write the value.</param>
		/// <param name="value">The value.</param>
		/// <remarks>The value may be a Action<System.IO.TextWriter>, as returned by Razor helpers.</remarks>
		protected static void WriteTo (System.IO.TextWriter writer, object value)
		{
			if (value == null)
				return;

			var write = value as Action<System.IO.TextWriter>;
			if (write != null) {
				write (writer);
				return;
			}

			//NOTE: a more sophisticated implementation would write safe and pre-escaped values directly to the
			//instead of double-escaping. See System.Web.IHtmlString in ASP.NET 4.0 for an example of this.
			writer.Write(System.Net.WebUtility.HtmlEncode (value.ToString ()));
		}

		// This method is REQUIRED, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to the template output.
		/// </summary>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		protected void WriteAttribute (string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			WriteAttributeTo (__razor_writer, name, prefix, suffix, values);
		}

		// This method is REQUIRED if the template contains any Razor helpers, but you may choose to implement it differently
		//
		/// <summary>
		/// Conditionally writes an attribute to a TextWriter.
		/// </summary>
		/// <param name="writer">The TextWriter to which to write the attribute.</param>
		/// <param name="name">The name of the attribute.</param>
		/// <param name="prefix">The prefix of the attribute.</param>
		/// <param name="suffix">The suffix of the attribute.</param>
		/// <param name="values">Attribute values, each specifying a prefix, value and whether it's a literal.</param>
		///<remarks>Used by Razor helpers to write attributes.</remarks>
		protected static void WriteAttributeTo (System.IO.TextWriter writer, string name, string prefix, string suffix, params Tuple<string,object,bool>[] values)
		{
			// this is based on System.Web.WebPages.WebPageExecutingBase
			// Copyright (c) Microsoft Open Technologies, Inc.
			// Licensed under the Apache License, Version 2.0
			if (values.Length == 0) {
				// Explicitly empty attribute, so write the prefix and suffix
				writer.Write (prefix);
				writer.Write (suffix);
				return;
			}

			bool first = true;
			bool wroteSomething = false;

			for (int i = 0; i < values.Length; i++) {
				Tuple<string,object,bool> attrVal = values [i];
				string attPrefix = attrVal.Item1;
				object value = attrVal.Item2;
				bool isLiteral = attrVal.Item3;

				if (value == null) {
					// Nothing to write
					continue;
				}

				// The special cases here are that the value we're writing might already be a string, or that the 
				// value might be a bool. If the value is the bool 'true' we want to write the attribute name instead
				// of the string 'true'. If the value is the bool 'false' we don't want to write anything.
				//
				// Otherwise the value is another object (perhaps an IHtmlString), and we'll ask it to format itself.
				string stringValue;
				bool? boolValue = value as bool?;
				if (boolValue == true) {
					stringValue = name;
				} else if (boolValue == false) {
					continue;
				} else {
					stringValue = value as string;
				}

				if (first) {
					writer.Write (prefix);
					first = false;
				} else {
					writer.Write (attPrefix);
				}

				if (isLiteral) {
					writer.Write (stringValue ?? value);
				} else {
					WriteTo (writer, stringValue ?? value);
				}
				wroteSomething = true;
			}
			if (wroteSomething) {
				writer.Write (suffix);
			}
		}
		// This method is REQUIRED. The generated Razor subclass will override it with the generated code.
		//
		///<summary>Executes the template, writing output to the Write and WriteLiteral methods.</summary>.
		///<remarks>Not intended to be called directly. Call the Generate method instead.</remarks>
		public abstract void Execute ();

}
}
#pragma warning restore 1591
