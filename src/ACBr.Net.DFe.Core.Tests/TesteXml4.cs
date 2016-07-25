﻿using ACBr.Net.DFe.Core.Attributes;
using ACBr.Net.DFe.Core.Collection;
using ACBr.Net.DFe.Core.Serializer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACBr.Net.DFe.Core.Tests
{
	public class TesteXml4
	{
		[DFeAttribute(TipoCampo.Int, "id", Id = "AT4", Min = 2, Max = 2, Ocorrencias = 0)]
		public int Id { get; set; }

		[DFeElement(TipoCampo.Custom, "custom3", Id = "ST4", Min = 0, Max = 19, Ocorrencias = 0)]
		public string TestString { get; set; }

		[DFeElement(TipoCampo.De3, "decimal2", Id = "DC4", Min = 0, Max = 9, Ocorrencias = 0)]
		public decimal TestDecimal { get; set; }

		private string SerializeTestString()
		{
			return $"{TestString} || SerializeTestString {Id:00}";
		}

		private object DeserializeTestString(string value)
		{
			return value.Replace($" || SerializeTestString {Id:00}", string.Empty);
		}

		#region Overrides

		public override string ToString()
		{
			var props = GetType().GetProperties();
			var builder = new StringBuilder();
			foreach (var prop in props)
			{
				if (prop.PropertyType.IsArray || prop.PropertyType.IsAssignableFrom(typeof(IEnumerable))
					|| prop.PropertyType.IsAssignableFrom(typeof(ICollection)) ||
					(prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(List<>)) ||
					(prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(DFeCollection<>)))
				{
					var values = ((IEnumerable<object>)prop.GetValue(this, null) ?? new object[0]).ToArray();
					foreach (var value in values)
						builder.AppendLine($"{prop.Name}: {value.GetType()}{Environment.NewLine}{value}");
				}
				else
				{
					var value = prop.GetValue(this, null);
					builder.AppendLine($"{prop.Name}: {value}");
				}
			}

			return builder.ToString();
		}

		#endregion Overrides
	}
}