// KRLab - Free class diagram editor
// Copyright (C) 2006-2009 Balazs Tihanyi
// 
// This program is free software; you can redistribute it and/or modify it under 
// the terms of the GNU General Public License as published by the Free Software 
// Foundation; either version 3 of the License, or (at your option) any later version.
// 
// This program is distributed in the hope that it will be useful, but WITHOUT 
// ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS 
// FOR A PARTICULAR PURPOSE. See the GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License along with 
// this program; if not, write to the Free Software Foundation, Inc., 
// 59 Temple Place, Suite 330, Boston, MA  02111-1307  USA

using System;
using KRLab.Translations;

namespace KRLab.Core
{
	public sealed class CommentRelation : Relationship
	{
		Comment comment;
		IEntity entity;

        public static readonly string Rational = RelationshipType.Comment.ToString(); 

		/// <exception cref="ArgumentNullException">
		/// <paramref name="comment"/> is null.-or-
		/// <paramref name="entity"/> is null.
		/// </exception>
		public CommentRelation(Comment comment, IEntity entity)
		{
			if (comment == null)
				throw new ArgumentNullException("comment");
			if (entity == null)
				throw new ArgumentNullException("entity");

			this.comment = comment;
			this.entity = entity;
			Attach();
		}

		public override RelationshipType RelationshipType
		{
			get { return RelationshipType.Comment; }
		}

		public override IEntity First
		{
			get { return comment; }
			protected set { comment = (Comment) value; }
		}

		public override IEntity Second
		{
			get { return entity; }
			protected set { entity = value; }
		}

		public CommentRelation Clone(Comment comment, IEntity entity)
		{
			CommentRelation relationship = new CommentRelation(comment, entity);
			relationship.CopyFrom(this);
			return relationship;
		}

		public override string ToString()
		{
			return string.Format("{0}: {1} --- {2}",
				Strings.Comment, comment.ToString(), entity.Name);
		}
	}
}
