#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;
using Telerik.OpenAccess;
using Telerik.OpenAccess.Metadata;
using Telerik.OpenAccess.Data.Common;
using Telerik.OpenAccess.Metadata.Fluent;
using Telerik.OpenAccess.Metadata.Fluent.Advanced;


namespace RecipiesModelNS	
{
	public partial class EmailTemplate
	{
		private int _emailTemplateId;
		public virtual int EmailTemplateId 
		{ 
		    get
		    {
		        return this._emailTemplateId;
		    }
		    set
		    {
		        this._emailTemplateId = value;
		    }
		}
		
		private string _from;
		public virtual string From 
		{ 
		    get
		    {
		        return this._from;
		    }
		    set
		    {
		        this._from = value;
		    }
		}
		
		private string _cc;
		public virtual string Cc 
		{ 
		    get
		    {
		        return this._cc;
		    }
		    set
		    {
		        this._cc = value;
		    }
		}
		
		private string _bcc;
		public virtual string Bcc 
		{ 
		    get
		    {
		        return this._bcc;
		    }
		    set
		    {
		        this._bcc = value;
		    }
		}
		
		private string _subject;
		public virtual string Subject 
		{ 
		    get
		    {
		        return this._subject;
		    }
		    set
		    {
		        this._subject = value;
		    }
		}
		
		private string _textBody;
		public virtual string TextBody 
		{ 
		    get
		    {
		        return this._textBody;
		    }
		    set
		    {
		        this._textBody = value;
		    }
		}
		
		private string _htmlBody;
		public virtual string HtmlBody 
		{ 
		    get
		    {
		        return this._htmlBody;
		    }
		    set
		    {
		        this._htmlBody = value;
		    }
		}
		
		private string _attachmentName;
		public virtual string AttachmentName 
		{ 
		    get
		    {
		        return this._attachmentName;
		    }
		    set
		    {
		        this._attachmentName = value;
		    }
		}
		
		private bool _isDefault;
		public virtual bool IsDefault 
		{ 
		    get
		    {
		        return this._isDefault;
		    }
		    set
		    {
		        this._isDefault = value;
		    }
		}
		
		private DateTime? _modifiedDate;
		public virtual DateTime? ModifiedDate 
		{ 
		    get
		    {
		        return this._modifiedDate;
		    }
		    set
		    {
		        this._modifiedDate = value;
		    }
		}
		
		private string _modifiedByUser;
		public virtual string ModifiedByUser 
		{ 
		    get
		    {
		        return this._modifiedByUser;
		    }
		    set
		    {
		        this._modifiedByUser = value;
		    }
		}
		
	}
}
