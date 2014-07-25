using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Language.StandardClassification;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace EditorConfig
{
    class EditorConfigClassifier : IClassifier
    {
        private static Regex _rxKeywords = new Regex(@"(?<=\=\s?)([a-zA-Z0-9-]+)\b", RegexOptions.Compiled);
        private static Regex _rxIdentifier = new Regex(@"^\w+(?=\=?)", RegexOptions.Compiled);
        private static Regex _rxString = new Regex(@"\[([^\]]+)\]", RegexOptions.Compiled);
        private static Regex _rxComment = new Regex(@"#.*", RegexOptions.Compiled);
        private Dictionary<Regex, IClassificationType> _map;

        public EditorConfigClassifier(IClassificationTypeRegistryService registry)
        {
            _map = new Dictionary<Regex, IClassificationType>
            {
                {_rxComment, registry.GetClassificationType(PredefinedClassificationTypeNames.Comment)},
                {_rxString, registry.GetClassificationType(PredefinedClassificationTypeNames.String)},
                {_rxIdentifier, registry.GetClassificationType(PredefinedClassificationTypeNames.SymbolDefinition)},
                {_rxKeywords, registry.GetClassificationType(PredefinedClassificationTypeNames.Literal)},
            };
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            IList<ClassificationSpan> list = new List<ClassificationSpan>();
            string text = span.GetText(); // the span is always a single line

            foreach (Regex regex in _map.Keys)
                foreach (Match match in regex.Matches(text))
                {
                    var str = new SnapshotSpan(span.Snapshot, span.Start.Position + match.Index, match.Length);

                    // Make sure we don't double classify
                    if (list.Any(s => s.Span.IntersectsWith(str)))
                        continue;

                    list.Add(new ClassificationSpan(str, _map[regex]));
                }

            return list;
        }

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged
        {
            add { }
            remove { }
        }
    }
}