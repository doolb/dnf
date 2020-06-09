using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Text;

namespace System.Drawing
{
    /// <summary>Converts rectangles from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" />.</summary>
    // Token: 0x02000048 RID: 72
    public class RectangleConverter : TypeConverter
    {
        /// <summary>Determines if this converter can convert an object in the given source type to the native type of the converter.</summary>
        /// <param name="context">A formatter context. This object can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="sourceType">The type you want to convert from. </param>
        /// <returns>This method returns <see langword="true" /> if this object can perform the conversion; otherwise, <see langword="false" />.</returns>
        // Token: 0x06000718 RID: 1816 RVA: 0x00010403 File Offset: 0x0000E603
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> object that provides a format context. This can be <see langword="null" />, so you should always check. Also, properties on the context object can also return <see langword="null" />.</param>
        /// <param name="destinationType">A <see cref="T:System.Type" /> object that represents the type you want to convert to. </param>
        /// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
        // Token: 0x06000719 RID: 1817 RVA: 0x00010421 File Offset: 0x0000E621
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>Converts the given object to a <see cref="T:System.Drawing.Rectangle" /> object.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        /// <param name="value">The object to convert. </param>
        /// <returns>The converted object. </returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
        // Token: 0x0600071A RID: 1818 RVA: 0x00010454 File Offset: 0x0000E654
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            string text = value as string;
            if (text == null) {
                return base.ConvertFrom(context, culture, value);
            }
            if (culture == null) {
                culture = CultureInfo.CurrentCulture;
            }
            string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
            Int32Converter int32Converter = new Int32Converter();
            int[] array2 = new int[array.Length];
            for (int i = 0; i < array2.Length; i++) {
                array2[i] = (int)int32Converter.ConvertFromString(context, culture, array[i]);
            }
            if (array.Length != 4) {
                throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"x,y,Width,Height.\"");
            }
            return new Rectangle(array2[0], array2[1], array2[2], array2[3]);
        }

        /// <summary>Converts the specified object to the specified type.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        /// <param name="value">The object to convert. </param>
        /// <param name="destinationType">The type to convert the object to. </param>
        /// <returns>The converted object.</returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
        // Token: 0x0600071B RID: 1819 RVA: 0x00010500 File Offset: 0x0000E700
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (value is Rectangle) {
                Rectangle rectangle = (Rectangle)value;
                if (destinationType == typeof(string)) {
                    string listSeparator = culture.TextInfo.ListSeparator;
                    StringBuilder stringBuilder = new StringBuilder();
                    stringBuilder.Append(rectangle.X.ToString(culture));
                    stringBuilder.Append(listSeparator);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(rectangle.Y.ToString(culture));
                    stringBuilder.Append(listSeparator);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(rectangle.Width.ToString(culture));
                    stringBuilder.Append(listSeparator);
                    stringBuilder.Append(" ");
                    stringBuilder.Append(rectangle.Height.ToString(culture));
                    return stringBuilder.ToString();
                }
                if (destinationType == typeof(InstanceDescriptor)) {
                    return new InstanceDescriptor(typeof(Rectangle).GetConstructor(new Type[]
                    {
                        typeof(int),
                        typeof(int),
                        typeof(int),
                        typeof(int)
                    }), new object[]
                    {
                        rectangle.X,
                        rectangle.Y,
                        rectangle.Width,
                        rectangle.Height
                    });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>Creates an instance of this type given a set of property values for the object. This is useful for objects that are immutable but still want to provide changeable properties.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided. </param>
        /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from a call to the <see cref="M:System.Drawing.RectangleConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method. </param>
        /// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.</returns>
        // Token: 0x0600071C RID: 1820 RVA: 0x00010698 File Offset: 0x0000E898
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) {
            object obj = propertyValues["X"];
            object obj2 = propertyValues["Y"];
            object obj3 = propertyValues["Width"];
            object obj4 = propertyValues["Height"];
            if (obj == null || obj2 == null || obj3 == null || obj4 == null) {
                throw new ArgumentException("propertyValues");
            }
            int x = (int)obj;
            int y = (int)obj2;
            int width = (int)obj3;
            int height = (int)obj4;
            return new Rectangle(x, y, width, height);
        }

        /// <summary>Determines if changing a value on this object should require a call to <see cref="M:System.Drawing.RectangleConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> to create a new value.</summary>
        /// <param name="context">A type descriptor through which additional context can be provided. </param>
        /// <returns>This method returns <see langword="true" /> if <see cref="M:System.Drawing.RectangleConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> should be called when a change is made to one or more properties of this object; otherwise, <see langword="false" />.</returns>
        // Token: 0x0600071D RID: 1821 RVA: 0x00010716 File Offset: 0x0000E916
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
            return true;
        }

        /// <summary>Retrieves the set of properties for this type. By default, a type does not return any properties. </summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided. </param>
        /// <param name="value">The value of the object to get the properties for. </param>
        /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties. </param>
        /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
        // Token: 0x0600071E RID: 1822 RVA: 0x00010719 File Offset: 0x0000E919
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
            if (value is Rectangle) {
                return TypeDescriptor.GetProperties(value, attributes);
            }
            return base.GetProperties(context, value, attributes);
        }

        /// <summary>Determines if this object supports properties. By default, this is <see langword="false" />.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> through which additional context can be provided. </param>
        /// <returns>This method returns <see langword="true" /> if <see cref="M:System.Drawing.RectangleConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> should be called to find the properties of this object; otherwise, <see langword="false" />.</returns>
        // Token: 0x0600071F RID: 1823 RVA: 0x00010734 File Offset: 0x0000E934
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
            return true;
        }
    }
}
