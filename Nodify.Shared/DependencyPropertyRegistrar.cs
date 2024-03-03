using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Data;
using Expression = System.Linq.Expressions.Expression;

namespace Nodify.Shared
{
    public delegate void PropertyChangedCallback<in TOwner>( TOwner sender, DependencyPropertyChangedEventArgs e ) where TOwner : DependencyObject;
    public delegate Boolean ValidateValueCallback<in TValue>( TValue value );
    public delegate TValue CoerceValueCallback<in TOwner, TValue>( TOwner sender, TValue value );
    public static class DependencyPropertyRegistrar<TOwner> where TOwner : DependencyObject
    {
        public sealed class Property<TValue>
        {
            public delegate void ChangedCallback( TValue oldValue, TValue newValue );


            internal Property( String propertyName )
            {
                m_propertyName = propertyName;
            }


            public Property<TValue> Default( TValue value )
            {
                m_default = value;

                return this;
            }


            public Property<TValue> AffectsRender()
            {
                m_flags |= FrameworkPropertyMetadataOptions.AffectsRender;

                return this;
            }


            public Property<TValue> AffectsMeasure()
            {
                m_flags |= FrameworkPropertyMetadataOptions.AffectsMeasure;

                return this;
            }


            public Property<TValue> AffectsArrange()
            {
                m_flags |= FrameworkPropertyMetadataOptions.AffectsArrange;

                return this;
            }


            public Property<TValue> BindsTwoWayByDefault()
            {
                m_flags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;

                return this;
            }


            public Property<TValue> NotDataBindable()
            {
                m_flags |= FrameworkPropertyMetadataOptions.NotDataBindable;

                return this;
            }


            public Property<TValue> Inheritable()
            {
                m_flags |= FrameworkPropertyMetadataOptions.Inherits;

                return this;
            }


            public Property<TValue> Journal()
            {
                m_flags |= FrameworkPropertyMetadataOptions.Journal;

                return this;
            }


            public Property<TValue> UpdateSource( UpdateSourceTrigger trigger )
            {
                m_updateSourceTrigger = trigger;

                return this;
            }


            public Property<TValue> OnChange( PropertyChangedCallback<TOwner> callback )
            {
                m_changeCallback = callback;

                return this;
            }


            public Property<TValue> OnChange( Expression<Func<TOwner, ChangedCallback>> expression )
            {
                var unaryExpression = (UnaryExpression)expression.Body;
                var createDelegateExpression = (MethodCallExpression)unaryExpression.Operand;
                var method = (MethodInfo)( (ConstantExpression)createDelegateExpression.Object ).Value;

                var instance = Expression.Parameter( typeof( TOwner ), "instance" );
                var oldValue = Expression.Parameter( typeof( TValue ), "oldValue" );
                var newValue = Expression.Parameter( typeof( TValue ), "newValue" );

                var methodCall = Expression.Call( instance, method, oldValue, newValue );
                var callback = Expression.Lambda<Action<TOwner, TValue, TValue>>( methodCall, instance, oldValue, newValue ).Compile();

                m_changeCallback = new PropertyChangedCallback<TOwner>(
                    ( sender, e ) => callback( sender, (TValue)e.OldValue, (TValue)e.NewValue ) );

                return this;
            }


            public Property<TValue> Coerce( CoerceValueCallback<TOwner, TValue> callback )
            {
                if ( typeof( TValue ).IsValueType )
                {
                    // Create a callback that tries to avoid reboxing the original value if the coercion didn't change it
                    m_coerceCallback = ( sender, baseValue ) =>
                    {
                        var unboxedBaseValue = (TValue)baseValue;
                        var coercedValue = callback( (TOwner)sender, unboxedBaseValue );
                        if ( EqualityComparer<TValue>.Default.Equals( coercedValue, unboxedBaseValue ) )
                        {
                            // Avoid creating new boxed instance
                            return baseValue;
                        }
                        return coercedValue;
                    };
                }
                else
                {
                    m_coerceCallback = ( sender, args ) => callback( (TOwner)sender, (TValue)args );
                }

                return this;
            }


            public Property<TValue> CoerceObject( CoerceValueCallback<TOwner, Object> callback )
            {
                m_coerceCallback = ( sender, args ) => callback( (TOwner)sender, args );

                return this;
            }


            public Property<TValue> Validate( ValidateValueCallback<TValue> callback )
            {
                m_validateCallback = callback;

                return this;
            }


            public static implicit operator DependencyProperty( Property<TValue> instance )
            {
                var propertyName = instance.m_propertyName;
                var validateCallback = instance.m_validateCallback;

                return DependencyProperty.Register(
                    propertyName,
                    typeof( TValue ),
                    typeof( TOwner ),
                    instance.CreateMetadata(),
                    validateCallback != null ? new ValidateValueCallback( x => validateCallback( (TValue)x ) ) : null );
            }


            public static implicit operator DependencyPropertyKey( Property<TValue> instance )
            {
                var propertyName = instance.m_propertyName;
                var validateCallback = instance.m_validateCallback;

                return DependencyProperty.RegisterReadOnly(
                    propertyName,
                    typeof( TValue ),
                    typeof( TOwner ),
                    instance.CreateMetadata(),
                    validateCallback != null ? new ValidateValueCallback( x => validateCallback( (TValue)x ) ) : null );
            }


            private FrameworkPropertyMetadata CreateMetadata()
            {
                // Capture delegates
                var changeCallback = m_changeCallback;
                var coerceCallback = m_coerceCallback;
                var metadata = new FrameworkPropertyMetadata((object)m_default, m_flags );

                if ( changeCallback != null )
                {
                    metadata.PropertyChangedCallback = new PropertyChangedCallback( ( sender, args ) => changeCallback( (TOwner)sender, args ) );
                }

                if ( coerceCallback != null )
                {
                    metadata.CoerceValueCallback = coerceCallback;
                }

                if ( m_updateSourceTrigger != null )
                {
                    metadata.DefaultUpdateSourceTrigger = m_updateSourceTrigger.Value;
                }

                return metadata;
            }


            private readonly String m_propertyName;
            private TValue m_default;
            private PropertyChangedCallback<TOwner> m_changeCallback;
            private FrameworkPropertyMetadataOptions m_flags;
            private ValidateValueCallback<TValue> m_validateCallback;
            private CoerceValueCallback m_coerceCallback;
            private UpdateSourceTrigger? m_updateSourceTrigger;
        }


        public sealed class AttachedProperty<TValue>
        {
            internal AttachedProperty( String propertyName )
            {
                _mPropertyName = propertyName;
            }


            public AttachedProperty<TValue> Default( TValue value )
            {
                _mDefault = value;

                return this;
            }


            public AttachedProperty<TValue> AffectsRender()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.AffectsRender;

                return this;
            }


            public AttachedProperty<TValue> AffectsMeasure()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.AffectsMeasure;

                return this;
            }


            public AttachedProperty<TValue> AffectsArrange()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.AffectsArrange;

                return this;
            }


            public AttachedProperty<TValue> BindsTwoWayByDefault()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.BindsTwoWayByDefault;

                return this;
            }


            public AttachedProperty<TValue> NotDataBindable()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.NotDataBindable;

                return this;
            }


            public AttachedProperty<TValue> Inheritable()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.Inherits;

                return this;
            }


            public AttachedProperty<TValue> Journal()
            {
                _mFlags |= FrameworkPropertyMetadataOptions.Journal;

                return this;
            }


            public AttachedProperty<TValue> UpdateSource( UpdateSourceTrigger trigger )
            {
                _mUpdateSourceTrigger = trigger;

                return this;
            }


            public AttachedProperty<TValue> OnChange( PropertyChangedCallback callback )
            {
                _mChangeCallback = callback;

                return this;
            }


            public AttachedProperty<TValue> Coerce( CoerceValueCallback callback )
            {
                _mCoerceCallback = callback;

                return this;
            }


            public AttachedProperty<TValue> Validate( ValidateValueCallback callback )
            {
                _mValidateCallback = callback;

                return this;
            }


            public static implicit operator DependencyProperty( AttachedProperty<TValue> instance )
            {
                var propertyName = instance._mPropertyName;
                var validateCallback = instance._mValidateCallback;

                return DependencyProperty.RegisterAttached( propertyName,
                    typeof( TValue ),
                    typeof( TOwner ),
                    instance.CreateMetadata(),
                    validateCallback );
            }


            public static implicit operator DependencyPropertyKey( AttachedProperty<TValue> instance )
            {
                var propertyName = instance._mPropertyName;
                var validateCallback = instance._mValidateCallback;

                return DependencyProperty.RegisterAttachedReadOnly( propertyName,
                    typeof( TValue ),
                    typeof( TOwner ),
                    instance.CreateMetadata(),
                    validateCallback );
            }


            private FrameworkPropertyMetadata CreateMetadata()
            {
                var metadata = new FrameworkPropertyMetadata((object)_mDefault, _mFlags )
                {
                    PropertyChangedCallback = _mChangeCallback,
                    CoerceValueCallback = _mCoerceCallback
                };

                if ( _mUpdateSourceTrigger != null )
                {
                    metadata.DefaultUpdateSourceTrigger = _mUpdateSourceTrigger.Value;
                }

                return metadata;
            }


            private readonly String _mPropertyName;
            private TValue _mDefault;
            private PropertyChangedCallback _mChangeCallback;
            private FrameworkPropertyMetadataOptions _mFlags;
            private ValidateValueCallback _mValidateCallback;
            private CoerceValueCallback _mCoerceCallback;
            private UpdateSourceTrigger? _mUpdateSourceTrigger;
        }



        public static Type CurrentType => typeof( TOwner );


        public static Property<TProperty> RegisterProperty<TProperty>( Expression<Func<TOwner, TProperty>> property )
        {
            if ( property == null )
            {
                throw new ArgumentNullException( nameof( property ) );
            }

            var propertyInfo = (PropertyInfo)( (MemberExpression)property.Body ).Member;

            return new Property<TProperty>( propertyInfo.Name );
        }


        public static Property<TProperty> RegisterProperty<TProperty>( String propertyName = null, [CallerMemberName] String staticReadOnlyName = null )
        {
            if ( propertyName == null )
            {
                propertyName = staticReadOnlyName.Substring( 0, staticReadOnlyName.Length - "Property".Length );
            }
            else
            {
                Debug.Assert( propertyName + "Property" == staticReadOnlyName );
            }

            return new Property<TProperty>( propertyName );
        }


        public static AttachedProperty<TProperty> RegisterAttachedProperty<TProperty>( String propertyName = null, [CallerMemberName] String staticReadOnlyName = null )
        {
            if ( propertyName == null )
            {
                propertyName = staticReadOnlyName.Substring( 0, staticReadOnlyName.Length - "Property".Length );
            }
            else
            {
                Debug.Assert( propertyName + "Property" == staticReadOnlyName );
            }

            return new AttachedProperty<TProperty>( propertyName );
        }


        public static RoutedEvent RegisterEvent<THandler>( String eventName, RoutingStrategy strategy )
        {
            return EventManager.RegisterRoutedEvent( eventName, strategy, typeof( THandler ), CurrentType );
        }
    }
}