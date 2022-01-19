//// Decompiled with JetBrains decompiler
//// Type: System.Windows.Documents.Glyphs
//// Assembly: PresentationFramework, Version=6.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
//// MVID: 30BAF184-F5F1-437B-854B-D73B0C2FA233
//// Assembly location: C:\Program Files\dotnet\shared\Microsoft.WindowsDesktop.App\6.0.0\PresentationFramework.dll

//using MS.Internal;
//using MS.Internal.Utility;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Globalization;
//using System.Windows.Markup;
//using System.Windows.Media;
//using System.Windows.Navigation;

//namespace System.Windows.Documents
//{
//    public sealed class Glyphs : FrameworkElement, IUriContext
//    {
//        public static readonly DependencyProperty FillProperty = DependencyProperty.Register(nameof(Fill), typeof(Brush), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.None, new PropertyChangedCallback((object)null, __methodptr(FillChanged)), (CoerceValueCallback)null));
//        public static readonly DependencyProperty IndicesProperty = DependencyProperty.Register(nameof(Indices), typeof(string), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty UnicodeStringProperty = DependencyProperty.Register(nameof(UnicodeString), typeof(string), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)string.Empty, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty CaretStopsProperty = DependencyProperty.Register(nameof(CaretStops), typeof(string), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)string.Empty, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty FontRenderingEmSizeProperty = DependencyProperty.Register(nameof(FontRenderingEmSize), typeof(double), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)0.0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty OriginXProperty = DependencyProperty.Register(nameof(OriginX), typeof(double), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(OriginPropertyChanged))));
//        public static readonly DependencyProperty OriginYProperty = DependencyProperty.Register(nameof(OriginY), typeof(double), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)double.NaN, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(OriginPropertyChanged))));
//        public static readonly DependencyProperty FontUriProperty = DependencyProperty.Register(nameof(FontUri), typeof(Uri), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty StyleSimulationsProperty = DependencyProperty.Register(nameof(StyleSimulations), typeof(StyleSimulations), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)StyleSimulations.None, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty IsSidewaysProperty = DependencyProperty.Register(nameof(IsSideways), typeof(bool), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)false, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty BidiLevelProperty = DependencyProperty.Register(nameof(BidiLevel), typeof(int), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)0, FrameworkPropertyMetadataOptions.AffectsMeasure | FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        public static readonly DependencyProperty DeviceFontNameProperty = DependencyProperty.Register(nameof(DeviceFontName), typeof(string), typeof(Glyphs), (PropertyMetadata)new FrameworkPropertyMetadata((object)null, FrameworkPropertyMetadataOptions.AffectsRender, new PropertyChangedCallback((object)null, __methodptr(GlyphRunPropertyChanged))));
//        private Glyphs.LayoutDependentGlyphRunProperties _glyphRunProperties;
//        private GlyphRun _measurementGlyphRun;
//        private Point _glyphRunOrigin;
//        private const double EmMultiplier = 100.0;

//        public GlyphRun ToGlyphRun()
//        {
//            this.ComputeMeasurementGlyphRunAndOrigin();
//            return this._measurementGlyphRun == null ? (GlyphRun)null : this._measurementGlyphRun;
//        }

//        Uri IUriContext.BaseUri
//        {
//            get => (Uri)this.GetValue(BaseUriHelper.BaseUriProperty);
//            set => this.SetValue(BaseUriHelper.BaseUriProperty, (object)value);
//        }

//        protected override Size ArrangeOverride(Size finalSize)
//        {
//            base.ArrangeOverride(finalSize);
//            Rect rect = this._measurementGlyphRun == null ? Rect.Empty : this._measurementGlyphRun.ComputeInkBoundingBox();
//            if (!rect.IsEmpty)
//            {
//                rect.X += this._glyphRunOrigin.X;
//                rect.Y += this._glyphRunOrigin.Y;
//            }
//            return finalSize;
//        }

//        protected override void OnRender(DrawingContext context)
//        {
//            if (this._glyphRunProperties == null || this._measurementGlyphRun == null)
//                return;
//            context.PushGuidelineY1(this._glyphRunOrigin.Y);
//            try
//            {
//                context.DrawGlyphRun(this.Fill, this._measurementGlyphRun);
//            }
//            finally
//            {
//                context.Pop();
//            }
//        }

//        protected override Size MeasureOverride(Size constraint)
//        {
//            this.ComputeMeasurementGlyphRunAndOrigin();
//            if (this._measurementGlyphRun == null)
//                return new Size();
//            Rect alignmentBox = this._measurementGlyphRun.ComputeAlignmentBox();
//            alignmentBox.Offset(this._glyphRunOrigin.X, this._glyphRunOrigin.Y);
//            return new Size(Math.Max(0.0, alignmentBox.Right), Math.Max(0.0, alignmentBox.Bottom));
//        }

//        private void ComputeMeasurementGlyphRunAndOrigin()
//        {
//            if (this._glyphRunProperties == null)
//            {
//                this._measurementGlyphRun = (GlyphRun)null;
//                this.ParseGlyphRunProperties();
//                if (this._glyphRunProperties == null)
//                    return;
//            }
//            else if (this._measurementGlyphRun != null)
//                return;
//            bool flag1 = (this.BidiLevel & 1) == 0;
//            int num = !DoubleUtil.IsNaN(this.OriginX) ? 1 : 0;
//            bool flag2 = !DoubleUtil.IsNaN(this.OriginY);
//            bool flag3 = false;
//            Rect rect = new Rect();
//            if ((num & (flag2 ? 1 : 0) & (flag1 ? 1 : 0)) != 0)
//            {
//                this._measurementGlyphRun = this._glyphRunProperties.CreateGlyphRun(new Point(this.OriginX, this.OriginY), this.Language);
//                flag3 = true;
//            }
//            else
//            {
//                this._measurementGlyphRun = this._glyphRunProperties.CreateGlyphRun(new Point(), this.Language);
//                rect = this._measurementGlyphRun.ComputeAlignmentBox();
//            }
//            this._glyphRunOrigin.X = num == 0 ? (flag1 ? 0.0 : rect.Width) : this.OriginX;
//            this._glyphRunOrigin.Y = !flag2 ? -rect.Y : this.OriginY;
//            if (flag3)
//                return;
//            this._measurementGlyphRun = this._glyphRunProperties.CreateGlyphRun(this._glyphRunOrigin, this.Language);
//        }

//        private void ParseCaretStops(
//          Glyphs.LayoutDependentGlyphRunProperties glyphRunProperties)
//        {
//            string caretStops = this.CaretStops;
//            if (string.IsNullOrEmpty(caretStops))
//            {
//                glyphRunProperties.caretStops = (IList<bool>)null;
//            }
//            else
//            {
//                bool[] flagArray = new bool[string.IsNullOrEmpty(glyphRunProperties.unicodeString) ? (glyphRunProperties.clusterMap == null || glyphRunProperties.clusterMap.Length == 0 ? glyphRunProperties.glyphIndices.Length + 1 : glyphRunProperties.clusterMap.Length + 1) : glyphRunProperties.unicodeString.Length + 1];
//                int index1 = 0;
//                foreach (char c in caretStops)
//                {
//                    if (!char.IsWhiteSpace(c))
//                    {
//                        int num;
//                        if ('0' <= c && c <= '9')
//                            num = (int)c - 48;
//                        else if ('a' <= c && c <= 'f')
//                        {
//                            num = (int)c - 97 + 10;
//                        }
//                        else
//                        {
//                            if ('A' > c || c > 'F')
//                                throw new ArgumentException(SR.Get("GlyphsCaretStopsContainsHexDigits"), "CaretStops");
//                            num = (int)c - 65 + 10;
//                        }
//                        if ((num & 8) != 0)
//                        {
//                            if (index1 >= flagArray.Length)
//                                throw new ArgumentException(SR.Get("GlyphsCaretStopsLengthCorrespondsToUnicodeString"), "CaretStops");
//                            flagArray[index1] = true;
//                        }
//                        int index2 = index1 + 1;
//                        if ((num & 4) != 0)
//                        {
//                            if (index2 >= flagArray.Length)
//                                throw new ArgumentException(SR.Get("GlyphsCaretStopsLengthCorrespondsToUnicodeString"), "CaretStops");
//                            flagArray[index2] = true;
//                        }
//                        int index3 = index2 + 1;
//                        if ((num & 2) != 0)
//                        {
//                            if (index3 >= flagArray.Length)
//                                throw new ArgumentException(SR.Get("GlyphsCaretStopsLengthCorrespondsToUnicodeString"), "CaretStops");
//                            flagArray[index3] = true;
//                        }
//                        int index4 = index3 + 1;
//                        if ((num & 1) != 0)
//                        {
//                            if (index4 >= flagArray.Length)
//                                throw new ArgumentException(SR.Get("GlyphsCaretStopsLengthCorrespondsToUnicodeString"), "CaretStops");
//                            flagArray[index4] = true;
//                        }
//                        index1 = index4 + 1;
//                    }
//                }
//                while (index1 < flagArray.Length)
//                    flagArray[index1++] = true;
//                glyphRunProperties.caretStops = (IList<bool>)flagArray;
//            }
//        }

//        private void ParseGlyphRunProperties()
//        {
//            Glyphs.LayoutDependentGlyphRunProperties glyphRunProperties = (Glyphs.LayoutDependentGlyphRunProperties)null;
//            Uri uri = this.FontUri;
//            if (uri != (Uri)null)
//            {
//                if (string.IsNullOrEmpty(this.UnicodeString) && string.IsNullOrEmpty(this.Indices))
//                    throw new ArgumentException(SR.Get("GlyphsUnicodeStringAndIndicesCannotBothBeEmpty"));
//                glyphRunProperties = new Glyphs.LayoutDependentGlyphRunProperties(this.GetDpi().PixelsPerDip);
//                if (!uri.IsAbsoluteUri)
//                    uri = BindUriHelper.GetResolvedUri(BaseUriHelper.GetBaseUri((DependencyObject)this), uri);
//                glyphRunProperties.glyphTypeface = new GlyphTypeface(uri, this.StyleSimulations);
//                glyphRunProperties.unicodeString = this.UnicodeString;
//                glyphRunProperties.sideways = this.IsSideways;
//                glyphRunProperties.deviceFontName = this.DeviceFontName;
//                List<Glyphs.ParsedGlyphData> parsedGlyphs;
//                int glyphsProperty = this.ParseGlyphsProperty(glyphRunProperties.glyphTypeface, glyphRunProperties.unicodeString, glyphRunProperties.sideways, out parsedGlyphs, out glyphRunProperties.clusterMap);
//                glyphRunProperties.glyphIndices = new ushort[glyphsProperty];
//                glyphRunProperties.advanceWidths = new double[glyphsProperty];
//                this.ParseCaretStops(glyphRunProperties);
//                glyphRunProperties.glyphOffsets = (Point[])null;
//                int index = 0;
//                glyphRunProperties.fontRenderingSize = this.FontRenderingEmSize;
//                glyphRunProperties.bidiLevel = this.BidiLevel;
//                double num = glyphRunProperties.fontRenderingSize / 100.0;
//                foreach (Glyphs.ParsedGlyphData parsedGlyphData in parsedGlyphs)
//                {
//                    glyphRunProperties.glyphIndices[index] = parsedGlyphData.glyphIndex;
//                    glyphRunProperties.advanceWidths[index] = parsedGlyphData.advanceWidth * num;
//                    if (parsedGlyphData.offsetX != 0.0 || parsedGlyphData.offsetY != 0.0)
//                    {
//                        if (glyphRunProperties.glyphOffsets == null)
//                            glyphRunProperties.glyphOffsets = new Point[glyphsProperty];
//                        glyphRunProperties.glyphOffsets[index].X = parsedGlyphData.offsetX * num;
//                        glyphRunProperties.glyphOffsets[index].Y = parsedGlyphData.offsetY * num;
//                    }
//                    ++index;
//                }
//            }
//            this._glyphRunProperties = glyphRunProperties;
//        }

//        private static bool IsEmpty(ReadOnlySpan<char> s)
//        {
//            ReadOnlySpan<char> readOnlySpan = s;
//            for (int index = 0; index < readOnlySpan.Length; ++index)
//            {
//                if (!char.IsWhiteSpace(readOnlySpan[index]))
//                    return false;
//            }
//            return true;
//        }

//        private bool ReadGlyphIndex(
//          ReadOnlySpan<char> valueSpec,
//          ref bool inCluster,
//          ref int glyphClusterSize,
//          ref int characterClusterSize,
//          ref ushort glyphIndex)
//        {
//            ReadOnlySpan<char> s1 = valueSpec;
//            int num1 = valueSpec.IndexOf<char>('(');
//            if (num1 != -1)
//            {
//                for (int index = 0; index < num1; ++index)
//                {
//                    if (!char.IsWhiteSpace(valueSpec[index]))
//                        throw new ArgumentException(SR.Get("GlyphsClusterBadCharactersBeforeBracket"));
//                }
//                if (inCluster)
//                    throw new ArgumentException(SR.Get("GlyphsClusterNoNestedClusters"));
//                int num2 = valueSpec.IndexOf<char>(')');
//                if (num2 == -1 || num2 <= num1 + 1)
//                    throw new ArgumentException(SR.Get("GlyphsClusterNoMatchingBracket"));
//                int num3 = valueSpec.IndexOf<char>(':');
//                if (num3 == -1)
//                {
//                    ReadOnlySpan<char> s2 = valueSpec.Slice(num1 + 1, num2 - (num1 + 1));
//                    characterClusterSize = int.Parse(s2, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                    glyphClusterSize = 1;
//                }
//                else
//                {
//                    if (num3 <= num1 + 1 || num3 >= num2 - 1)
//                        throw new ArgumentException(SR.Get("GlyphsClusterMisplacedSeparator"));
//                    ReadOnlySpan<char> s3 = valueSpec.Slice(num1 + 1, num3 - (num1 + 1));
//                    characterClusterSize = int.Parse(s3, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                    ReadOnlySpan<char> s4 = valueSpec.Slice(num3 + 1, num2 - (num3 + 1));
//                    glyphClusterSize = int.Parse(s4, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                }
//                inCluster = true;
//                s1 = valueSpec.Slice(num2 + 1);
//            }
//            if (Glyphs.IsEmpty(s1))
//                return false;
//            glyphIndex = ushort.Parse(s1, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//            return true;
//        }

//        private static double GetAdvanceWidth(
//          GlyphTypeface glyphTypeface,
//          ushort glyphIndex,
//          bool sideways)
//        {
//            return (sideways ? glyphTypeface.AdvanceHeights[glyphIndex] : glyphTypeface.AdvanceWidths[glyphIndex]) * 100.0;
//        }

//        private ushort GetGlyphFromCharacter(GlyphTypeface glyphTypeface, char character)
//        {
//            ushort glyphFromCharacter;
//            glyphTypeface.CharacterToGlyphMap.TryGetValue((int)character, out glyphFromCharacter);
//            return glyphFromCharacter;
//        }

//        private static void SetClusterMapEntry(ushort[] clusterMap, int index, ushort value)
//        {
//            if (index < 0 || index >= clusterMap.Length)
//                throw new ArgumentException(SR.Get("GlyphsUnicodeStringIsTooShort"));
//            clusterMap[index] = value;
//        }

//        private int ParseGlyphsProperty(
//          GlyphTypeface fontFace,
//          string unicodeString,
//          bool sideways,
//          out List<Glyphs.ParsedGlyphData> parsedGlyphs,
//          out ushort[] clusterMap)
//        {
//            string indices = this.Indices;
//            int glyphsProperty = 0;
//            int index1 = 0;
//            int characterClusterSize = 1;
//            int glyphClusterSize = 1;
//            bool inCluster = false;
//            int num1;
//            if (!string.IsNullOrEmpty(unicodeString))
//            {
//                clusterMap = new ushort[unicodeString.Length];
//                num1 = unicodeString.Length;
//            }
//            else
//            {
//                clusterMap = (ushort[])null;
//                num1 = 8;
//            }
//            if (!string.IsNullOrEmpty(indices))
//                num1 = Math.Max(num1, indices.Length / 5);
//            parsedGlyphs = new List<Glyphs.ParsedGlyphData>(num1);
//            Glyphs.ParsedGlyphData parsedGlyphData = new Glyphs.ParsedGlyphData();
//            if (!string.IsNullOrEmpty(indices))
//            {
//                int num2 = 0;
//                int start = 0;
//                for (int index2 = 0; index2 <= indices.Length; ++index2)
//                {
//                    char ch = index2 < indices.Length ? indices[index2] : char.MinValue;
//                    switch (ch)
//                    {
//                        case ',':
//                        case ';':
//                            int length = index2 - start;
//                            ReadOnlySpan<char> readOnlySpan = indices.AsSpan(start, length);
//                            switch (num2)
//                            {
//                                case 0:
//                                    int num3 = inCluster ? 1 : 0;
//                                    if (!this.ReadGlyphIndex(readOnlySpan, ref inCluster, ref glyphClusterSize, ref characterClusterSize, ref parsedGlyphData.glyphIndex))
//                                    {
//                                        if (string.IsNullOrEmpty(unicodeString))
//                                            throw new ArgumentException(SR.Get("GlyphsIndexRequiredIfNoUnicode"));
//                                        if (unicodeString.Length <= index1)
//                                            throw new ArgumentException(SR.Get("GlyphsUnicodeStringIsTooShort"));
//                                        parsedGlyphData.glyphIndex = this.GetGlyphFromCharacter(fontFace, unicodeString[index1]);
//                                    }
//                                    if (num3 == 0 && clusterMap != null)
//                                    {
//                                        if (inCluster)
//                                        {
//                                            for (int index3 = index1; index3 < index1 + characterClusterSize; ++index3)
//                                                Glyphs.SetClusterMapEntry(clusterMap, index3, (ushort)glyphsProperty);
//                                        }
//                                        else
//                                            Glyphs.SetClusterMapEntry(clusterMap, index1, (ushort)glyphsProperty);
//                                    }
//                                    parsedGlyphData.advanceWidth = Glyphs.GetAdvanceWidth(fontFace, parsedGlyphData.glyphIndex, sideways);
//                                    break;
//                                case 1:
//                                    if (!Glyphs.IsEmpty(readOnlySpan))
//                                    {
//                                        parsedGlyphData.advanceWidth = double.Parse(readOnlySpan, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                                        if (parsedGlyphData.advanceWidth < 0.0)
//                                            throw new ArgumentException(SR.Get("GlyphsAdvanceWidthCannotBeNegative"));
//                                        break;
//                                    }
//                                    break;
//                                case 2:
//                                    if (!Glyphs.IsEmpty(readOnlySpan))
//                                    {
//                                        parsedGlyphData.offsetX = double.Parse(readOnlySpan, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                                        break;
//                                    }
//                                    break;
//                                case 3:
//                                    if (!Glyphs.IsEmpty(readOnlySpan))
//                                    {
//                                        parsedGlyphData.offsetY = double.Parse(readOnlySpan, provider: ((IFormatProvider)CultureInfo.InvariantCulture));
//                                        break;
//                                    }
//                                    break;
//                                default:
//                                    throw new ArgumentException(SR.Get("GlyphsTooManyCommas"));
//                            }
//                            ++num2;
//                            start = index2 + 1;
//                            break;
//                        default:
//                            if (index2 != indices.Length)
//                                break;
//                            goto case ',';
//                    }
//                    if (ch == ';' || index2 == indices.Length)
//                    {
//                        parsedGlyphs.Add(parsedGlyphData);
//                        parsedGlyphData = new Glyphs.ParsedGlyphData();
//                        if (inCluster)
//                        {
//                            --glyphClusterSize;
//                            if (glyphClusterSize == 0)
//                            {
//                                index1 += characterClusterSize;
//                                inCluster = false;
//                            }
//                        }
//                        else
//                            ++index1;
//                        ++glyphsProperty;
//                        num2 = 0;
//                        start = index2 + 1;
//                    }
//                }
//            }
//            if (unicodeString != null)
//            {
//                while (index1 < unicodeString.Length)
//                {
//                    if (inCluster)
//                        throw new ArgumentException(SR.Get("GlyphsIndexRequiredWithinCluster"));
//                    if (unicodeString.Length <= index1)
//                        throw new ArgumentException(SR.Get("GlyphsUnicodeStringIsTooShort"));
//                    parsedGlyphData.glyphIndex = this.GetGlyphFromCharacter(fontFace, unicodeString[index1]);
//                    parsedGlyphData.advanceWidth = Glyphs.GetAdvanceWidth(fontFace, parsedGlyphData.glyphIndex, sideways);
//                    parsedGlyphs.Add(parsedGlyphData);
//                    parsedGlyphData = new Glyphs.ParsedGlyphData();
//                    Glyphs.SetClusterMapEntry(clusterMap, index1, (ushort)glyphsProperty);
//                    ++index1;
//                    ++glyphsProperty;
//                }
//            }
//            return glyphsProperty;
//        }

//        private static void FillChanged(DependencyObject d, DependencyPropertyChangedEventArgs e) => ((UIElement)d).InvalidateVisual();

//        private static void GlyphRunPropertyChanged(
//          DependencyObject d,
//          DependencyPropertyChangedEventArgs e)
//        {
//            ((Glyphs)d)._glyphRunProperties = (Glyphs.LayoutDependentGlyphRunProperties)null;
//        }

//        private static void OriginPropertyChanged(
//          DependencyObject d,
//          DependencyPropertyChangedEventArgs e)
//        {
//            ((Glyphs)d)._measurementGlyphRun = (GlyphRun)null;
//        }

//        public Brush Fill
//        {
//            get => (Brush)this.GetValue(Glyphs.FillProperty);
//            set => this.SetValue(Glyphs.FillProperty, (object)value);
//        }

//        public string Indices
//        {
//            get => (string)this.GetValue(Glyphs.IndicesProperty);
//            set => this.SetValue(Glyphs.IndicesProperty, (object)value);
//        }

//        public string UnicodeString
//        {
//            get => (string)this.GetValue(Glyphs.UnicodeStringProperty);
//            set => this.SetValue(Glyphs.UnicodeStringProperty, (object)value);
//        }

//        public string CaretStops
//        {
//            get => (string)this.GetValue(Glyphs.CaretStopsProperty);
//            set => this.SetValue(Glyphs.CaretStopsProperty, (object)value);
//        }

//        [TypeConverter("System.Windows.FontSizeConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
//        public double FontRenderingEmSize
//        {
//            get => (double)this.GetValue(Glyphs.FontRenderingEmSizeProperty);
//            set => this.SetValue(Glyphs.FontRenderingEmSizeProperty, (object)value);
//        }

//        [TypeConverter("System.Windows.LengthConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
//        public double OriginX
//        {
//            get => (double)this.GetValue(Glyphs.OriginXProperty);
//            set => this.SetValue(Glyphs.OriginXProperty, (object)value);
//        }

//        [TypeConverter("System.Windows.LengthConverter, PresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35, Custom=null")]
//        public double OriginY
//        {
//            get => (double)this.GetValue(Glyphs.OriginYProperty);
//            set => this.SetValue(Glyphs.OriginYProperty, (object)value);
//        }

//        public Uri FontUri
//        {
//            get => (Uri)this.GetValue(Glyphs.FontUriProperty);
//            set => this.SetValue(Glyphs.FontUriProperty, (object)value);
//        }

//        public StyleSimulations StyleSimulations
//        {
//            get => (StyleSimulations)this.GetValue(Glyphs.StyleSimulationsProperty);
//            set => this.SetValue(Glyphs.StyleSimulationsProperty, (object)value);
//        }

//        public bool IsSideways
//        {
//            get => (bool)this.GetValue(Glyphs.IsSidewaysProperty);
//            set => this.SetValue(Glyphs.IsSidewaysProperty, value);
//        }

//        public int BidiLevel
//        {
//            get => (int)this.GetValue(Glyphs.BidiLevelProperty);
//            set => this.SetValue(Glyphs.BidiLevelProperty, (object)value);
//        }

//        public string DeviceFontName
//        {
//            get => (string)this.GetValue(Glyphs.DeviceFontNameProperty);
//            set => this.SetValue(Glyphs.DeviceFontNameProperty, (object)value);
//        }

//        internal GlyphRun MeasurementGlyphRun
//        {
//            get
//            {
//                if (this._glyphRunProperties == null || this._measurementGlyphRun == null)
//                    this.ComputeMeasurementGlyphRunAndOrigin();
//                return this._measurementGlyphRun;
//            }
//        }

//        private class ParsedGlyphData
//        {
//            public ushort glyphIndex;
//            public double advanceWidth;
//            public double offsetX;
//            public double offsetY;
//        }

//        private class LayoutDependentGlyphRunProperties
//        {
//            public double fontRenderingSize;
//            public ushort[] glyphIndices;
//            public double[] advanceWidths;
//            public Point[] glyphOffsets;
//            public ushort[] clusterMap;
//            public bool sideways;
//            public int bidiLevel;
//            public GlyphTypeface glyphTypeface;
//            public string unicodeString;
//            public IList<bool> caretStops;
//            public string deviceFontName;
//            private float _pixelsPerDip;

//            public LayoutDependentGlyphRunProperties(double pixelsPerDip) => this._pixelsPerDip = (float)pixelsPerDip;

//            public GlyphRun CreateGlyphRun(Point origin, XmlLanguage language) => new GlyphRun(this.glyphTypeface, this.bidiLevel, this.sideways, this.fontRenderingSize, this._pixelsPerDip, (IList<ushort>)this.glyphIndices, origin, (IList<double>)this.advanceWidths, (IList<Point>)this.glyphOffsets, (IList<char>)this.unicodeString.ToCharArray(), this.deviceFontName, (IList<ushort>)this.clusterMap, this.caretStops, language);
//        }
//    }
//}
