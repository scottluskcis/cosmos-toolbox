using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CosmosToolbox.Core.Enums
{
    public interface IEnumeration
    {
        int Id { get;  }
        string Name { get;  }
        string DisplayValue { get; }
    }

    public abstract class Enumeration : IComparable, IEnumeration
    {
        public string Name { get; }

        public int Id { get; }

        public string DisplayValue { get; set; }

        protected Enumeration(int id, string name, string displayValue = null)
        {
            Id = id;
            Name = name;
            DisplayValue = displayValue;
        }

        public override string ToString() => Name;

        public static T GetById<T>(int id) where T : Enumeration
        {
            var enumeration = GetById(typeof(T), id);
            return CastOrCreate<T>(enumeration);
        }

        public static Enumeration GetById(Type type, int id)
        {
            var allEnumerations = GetAll(type);
            var enumerationById = allEnumerations.SingleOrDefault(p => p.Id == id);
            return enumerationById;
        }

        public static T GetByName<T>(string name, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
            where T : Enumeration
        {
            var enumeration = GetByName(typeof(T), name);
            return CastOrCreate<T>(enumeration);
        }

        public static Enumeration GetByName(Type type, string name, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            var allEnumerations = GetAll(type);
            var enumerationByName = allEnumerations
                .SingleOrDefault(p => 
                    string.Equals(p.Name, name, comparisonType));

            return enumerationByName;
        }

        public static IEnumerable<T> GetAll<T>() where T : Enumeration
        {
            var genericType = typeof(T);
            var enumerations = GetAll(genericType);
            return enumerations.Select(CastOrCreate<T>);
        }

        public static IEnumerable<Enumeration> GetAll(Type type)
        {
            var fields = type.GetFields(BindingFlags.Public
                                        | BindingFlags.Static
                                        | BindingFlags.DeclaredOnly);

            var enumerationFields = fields
                .Select(f => f.GetValue(null))
                .Cast<Enumeration>();

            var allEnumerations = (
                    type.BaseType != null && !type.IsAbstract
                        ? enumerationFields.Union(GetAll(type.BaseType))
                        : enumerationFields)
                .OrderBy(o => o.Id)
                .ToList();

            return allEnumerations;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Enumeration otherValue))
                return false;

            var typeMatches = GetType() == obj.GetType();
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Name != null ? Name.GetHashCode() : 0) * 397) ^ Id;
            }
        }

        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);

        public static bool TryParse<T>(string value, out T enumeration) where T : Enumeration
        {
            if (TryParse(typeof(T), value, out var valueEnumeration))
            {
                enumeration = CastOrCreate<T>(valueEnumeration);
                return true;
            }
            else
            {
                enumeration = null;
                return false;
            }
        }

        public static bool TryParse(Type type, string value, out Enumeration enumeration) 
        {
            enumeration = null;

            if (value == null)
            {
                return false;
            }

            if (int.TryParse(value, out var intValue))
            {
                enumeration = GetById(type, intValue);
            }

            enumeration ??= GetByName(type, value);

            return enumeration != null;
        }

        private static T CastOrCreate<T>(IEnumeration enumeration)
        {
            var genericType = typeof(T);
            if (genericType == enumeration.GetType())
            {
                return (T) enumeration;
            }

            var argTypes = new[]
            {
                typeof(int),
                typeof(string),
                typeof(string)
            };
            
            var constructor = genericType.GetConstructor(
                BindingFlags.NonPublic | BindingFlags.Instance, 
                null, 
                argTypes, 
                null);

            var argValues = new object[]
            {
                enumeration.Id,
                enumeration.Name,
                enumeration.DisplayValue
            };

            return (T)constructor?.Invoke(argValues);
        }

    }
}
