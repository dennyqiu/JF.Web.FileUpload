using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace JF.Web.FileUpload.Annotations {

	public class MaxFileSizeAttribute : ValidationAttribute {

		private readonly int _maxFileSize;

		public MaxFileSizeAttribute( int maxMbFileSize ) {
			_maxFileSize = maxMbFileSize * 8 * 1024 * 1024;
		}

		public override bool IsValid( object value ) {
			var file = value as HttpPostedFileBase;

			if ( file == null ) {
				return true;
			}

			return file.ContentLength <= _maxFileSize;
		}

		public override string FormatErrorMessage( string name ) {
			return base.FormatErrorMessage( _maxFileSize.ToString( CultureInfo.InvariantCulture ) );
		}

	}

}
