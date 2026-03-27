using System;
using System.Windows;

namespace Dream.Wpf;

public static class PropertyHelper
{
    public static void OverrideDefaultValue(DependencyProperty property, Type typeValue, object defaultValue)
    {
        FrameworkPropertyMetadata baseMetadata = (FrameworkPropertyMetadata)property.GetMetadata(typeValue);

        property.OverrideMetadata(typeValue, new FrameworkPropertyMetadata(defaultValue, ExtractFlags(baseMetadata),
                baseMetadata.PropertyChangedCallback, baseMetadata.CoerceValueCallback));
    }

    public static void OverrideDefaultValue(DependencyProperty property, Type typeValue, FrameworkPropertyMetadataOptions flags, object defaultValue)
    {
        FrameworkPropertyMetadata baseMetadata = (FrameworkPropertyMetadata)property.GetMetadata(typeValue);

        property.OverrideMetadata(typeValue, new FrameworkPropertyMetadata(defaultValue, flags,
            baseMetadata.PropertyChangedCallback, baseMetadata.CoerceValueCallback));
    }

    public static FrameworkPropertyMetadataOptions ExtractFlags(DependencyProperty property, Type typeValue)
    {
        FrameworkPropertyMetadata baseMetadata = (FrameworkPropertyMetadata)property.GetMetadata(typeValue);
        return ExtractFlags(baseMetadata);
    }

    public static FrameworkPropertyMetadataOptions ExtractFlags(FrameworkPropertyMetadata meta)
    {
        FrameworkPropertyMetadataOptions flags = FrameworkPropertyMetadataOptions.None;

        if (meta.AffectsRender) flags |= FrameworkPropertyMetadataOptions.AffectsRender;
        if (meta.AffectsMeasure) flags |= FrameworkPropertyMetadataOptions.AffectsMeasure;
        if (meta.AffectsArrange) flags |= FrameworkPropertyMetadataOptions.AffectsArrange;
        if (meta.Inherits) flags |= FrameworkPropertyMetadataOptions.Inherits;
        if (meta.BindsTwoWayByDefault) flags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;
        if (meta.Journal) flags |= FrameworkPropertyMetadataOptions.Journal;
        if (meta.SubPropertiesDoNotAffectRender) flags |= FrameworkPropertyMetadataOptions.SubPropertiesDoNotAffectRender;

        return flags;
    }
}
