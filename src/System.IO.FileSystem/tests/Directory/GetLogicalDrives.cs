﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Linq;
using System.Runtime.InteropServices;
using Xunit;

namespace System.IO.Tests
{
    public class Directory_GetLogicalDrives
    {
        [Fact]
        [PlatformSpecific(PlatformID.AnyUnix)]
        public void ThrowsPlatformNotSupported_Unix()
        {
            Assert.Throws<PlatformNotSupportedException>(() => Directory.GetLogicalDrives());
        }

        [Fact]
        [PlatformSpecific(PlatformID.Windows)]
        public void GetsValidDriveStrings_Windows()
        {
            string[] drives = Directory.GetLogicalDrives();
            Assert.NotEmpty(drives);
            Assert.All(drives, d => Assert.Matches(@"^[A-Z]:\\$", d));
        }
    }
}