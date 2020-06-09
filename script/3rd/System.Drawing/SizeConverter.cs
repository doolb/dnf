using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;

namespace System.Drawing
{
    /// <summary>The <see cref="T:System.Drawing.SizeConverter" /> class is used to convert from one data type to another. Access this class through the <see cref="T:System.ComponentModel.TypeDescriptor" /> object.</summary>
    // Token: 0x0200004D RID: 77
    public class SizeConverter : TypeConverter
    {
        /// <summary>Determines whether this converter can convert an object in the specified source type to the native type of the converter.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="sourceType">The type you want to convert from. </param>
        /// <returns>This method returns <see langword="true" /> if this object can perform the conversion.</returns>
        // Token: 0x060007AE RID: 1966 RVA: 0x00011D1F File Offset: 0x0000FF1F
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType) {
            return sourceType == typeof(string) || base.CanConvertFrom(context, sourceType);
        }

        /// <summary>Gets a value indicating whether this converter can convert an object to the given destination type using the context.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This can be <see langword="null" />, so always check. Also, properties on the context object can return <see langword="null" />.</param>
        /// <param name="destinationType">A <see cref="T:System.Type" /> that represents the type you want to convert to. </param>
        /// <returns>This method returns <see langword="true" /> if this converter can perform the conversion; otherwise, <see langword="false" />.</returns>
        // Token: 0x060007AF RID: 1967 RVA: 0x00011D3D File Offset: 0x0000FF3D
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType) {
            return destinationType == typeof(string) || destinationType == typeof(InstanceDescriptor) || base.CanConvertTo(context, destinationType);
        }

        /// <summary>Converts the specified object to the converter's native type.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        /// <param name="value">The object to convert. </param>
        /// <returns>The converted object. </returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
        // Token: 0x060007B0 RID: 1968 RVA: 0x00011D70 File Offset: 0x0000FF70
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value) {
            if (culture == null) {
                culture = CultureInfo.CurrentCulture;
            }
            string text = value as string;
            if (text == null) {
                return base.ConvertFrom(context, culture, value);
            }
            string[] array = text.Split(culture.TextInfo.ListSeparator.ToCharArray());
            Int32Converter int32Converter = new Int32Converter();
            int[] array2 = new int[array.Length];
            for (int i = 0; i < array2.Length; i++) {
                array2[i] = (int)int32Converter.ConvertFromString(context, culture, array[i]);
            }
            if (array.Length != 2) {
                throw new ArgumentException("Failed to parse Text(" + text + ") expected text in the format \"Width,Height.\"");
            }
            return new Size(array2[0], array2[1]);
        }

        /// <summary>Converts the specified object to the specified type.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that can be used to get additional information about the environment this converter is being called from. This may be <see langword="null" />, so you should always check. Also, properties on the context object may also return <see langword="null" />. </param>
        /// <param name="culture">An <see cref="T:System.Globalization.CultureInfo" /> object that contains culture specific information, such as the language, calendar, and cultural conventions associated with a specific culture. It is based on the RFC 1766 standard. </param>
        /// <param name="value">The object to convert. </param>
        /// <param name="destinationType">The type to convert the object to. </param>
        /// <returns>The converted object.</returns>
        /// <exception cref="T:System.NotSupportedException">The conversion cannot be completed.</exception>
        // Token: 0x060007B1 RID: 1969 RVA: 0x00011E14 File Offset: 0x00010014
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType) {
            if (culture == null) {
                culture = CultureInfo.CurrentCulture;
            }
            if (value is Size) {
                Size size = (Size)value;
                if (destinationType == typeof(string)) {
                    return size.Width.ToString(culture) + culture.TextInfo.ListSeparator + " " + size.Height.ToString(culture);
                }
                if (destinationType == typeof(InstanceDescriptor)) {
                    return new InstanceDescriptor(typeof(Size).GetConstructor(new Type[]
                    {
                        typeof(int),
                        typeof(int)
                    }), new object[]
                    {
                        size.Width,
                        size.Height
                    });
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        /// <summary>Creates an object of this type by using a specified set of property values for the object. This is useful for creating non-changeable objects that have changeable properties.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        /// <param name="propertyValues">A dictionary of new property values. The dictionary contains a series of name-value pairs, one for each property returned from the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method. </param>
        /// <returns>The newly created object, or <see langword="null" /> if the object could not be created. The default implementation returns <see langword="null" />.</returns>
        // Token: 0x060007B2 RID: 1970 RVA: 0x00011F00 File Offset: 0x00010100
        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues) {
            object obj = propertyValues["Width"];
            object obj2 = propertyValues["Height"];
            if (obj == null || obj2 == null) {
                throw new ArgumentException("propertyValues");
            }
            int width = (int)obj;
            int height = (int)obj2;
            return new Size(width, height);
        }

        /// <summary>Determines whether changing a value on this object should require a call to the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> method to create a new value.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.CreateInstance(System.ComponentModel.ITypeDescriptorContext,System.Collections.IDictionary)" /> object should be called when a change is made to one or more properties of this object.</returns>
        // Token: 0x060007B3 RID: 1971 RVA: 0x00011F4C File Offset: 0x0001014C
        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context) {
            return true;
        }

        /// <summary>Retrieves the set of properties for this type. By default, a type does not have any properties to return. </summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        /// <param name="value">The value of the object to get the properties for. </param>
        /// <param name="attributes">An array of <see cref="T:System.Attribute" /> objects that describe the properties. </param>
        /// <returns>The set of properties that should be exposed for this data type. If no properties should be exposed, this may return <see langword="null" />. The default implementation always returns <see langword="null" />.</returns>
        // Token: 0x060007B4 RID: 1972 RVA: 0x00011F4F File Offset: 0x0001014F
        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes) {
            if (value is Size) {
                return TypeDescriptor.GetProperties(value, attributes);
            }
            return base.GetProperties(context, value, attributes);
        }

        /// <summary>Determines whether this object supports properties. By default, this is <see langword="false" />.</summary>
        /// <param name="context">A <see cref="T:System.ComponentModel.TypeDescriptor" /> through which additional context can be provided. </param>
        /// <returns>
        ///     <see langword="true" /> if the <see cref="M:System.Drawing.SizeConverter.GetProperties(System.ComponentModel.ITypeDescriptorContext,System.Object,System.Attribute[])" /> method should be called to find the properties of this object.</returns>
        // Token: 0x060007B5 RID: 1973 RVA: 0x00011F6A File Offset: 0x0001016A
        public override bool GetPropertiesSupported(ITypeDescriptorContext context) {
            return true;
        }
    }
}
