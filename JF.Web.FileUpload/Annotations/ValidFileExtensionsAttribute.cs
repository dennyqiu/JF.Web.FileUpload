using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace JF.Web.FileUpload.Annotations {

	public class ValidFileExtensionsAttribute : DataTypeAttribute , IClientValidatable {

		private readonly FileExtensionsAttribute _innerAttribute = new FileExtensionsAttribute();

		public ValidFileExtensionsAttribute() : base( DataType.Upload ) {
			ErrorMessage = _innerAttribute.ErrorMessage;
		}

		public string Extensions {
			get {
				return _innerAttribute.Extensions;
			}
			set {
				_innerAttribute.Extensions = value;
			}
		}

		public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
			ModelMetadata metadata ,
			ControllerContext context ) {
			var rule = new ModelClientValidationRule {
				ValidationType = "accept" ,
				ErrorMessage = ErrorMessage
			};
			rule.ValidationParameters["exts"] = _innerAttribute.Extensions;
			yield return rule;
		}

		public override string FormatErrorMessage( string name ) {
			return _innerAttribute.FormatErrorMessage( name );
		}

		public override bool IsValid( object value ) {
			var file = value as HttpPostedFileBase;
			if ( file != null ) {
				return _innerAttribute.IsValid( file.FileName );
			}

			return _innerAttribute.IsValid( value );
		}

	}

}
