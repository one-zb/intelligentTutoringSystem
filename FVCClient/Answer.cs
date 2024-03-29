﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FVCClient
{
	public class Answer {
		public string Raw { get; private set; }
		public string Name { get; private set; }
		public string[] Args { get; private set; }

		public Answer(string raw, string name, string[] args) {
			Raw = raw;
			Name = name;
			Args = args;
		}

		public string GetDescription(string commandFormat, string firstArgFormat, string otherArgsFormat, string end) {
			string ret = string.Format(commandFormat, Name);
			if (Args.Length > 0)
				ret += string.Format(firstArgFormat, Args[0]);
			for (int i = 1; i < Args.Length; i++)
				ret += string.Format(otherArgsFormat, Args[i]);
			return ret + end;
		}

	}
}
