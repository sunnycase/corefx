// Copyright (c) Microsoft. All rights reserved.
// Licensed under the MIT license. See LICENSE file in the project root for full license information.

using System;
using Xunit;

namespace Microsoft.Win32.RegistryTests
{
    public class RegistryKey_DeleteSubKeyTree : TestSubKey
    {
        private const string TestKey = "BCL_TEST_42";

        public RegistryKey_DeleteSubKeyTree()
            : base(TestKey)
        {
        }

        [Fact]
        public void NegativeTests()
        {
            const string name = "Test";

            // Should throw if passed subkey name is null
            Assert.Throws<ArgumentNullException>(() => _testRegistryKey.DeleteSubKeyTree(null, throwOnMissingSubKey: true));
            Assert.Throws<ArgumentNullException>(() => _testRegistryKey.DeleteSubKeyTree(null, throwOnMissingSubKey: false));

            // Should throw if target subkey is system subkey and name is empty
            Assert.Throws<ArgumentException>(() => Registry.CurrentUser.DeleteSubKeyTree(string.Empty, throwOnMissingSubKey: false));

            // Should throw because subkey doesn't exists
            Assert.Throws<ArgumentException>(() => _testRegistryKey.DeleteSubKeyTree(name, throwOnMissingSubKey: true));

            // Should throw because RegistryKey is readonly
            using (var rk = _testRegistryKey.OpenSubKey(string.Empty, false))
            {
                Assert.Throws<UnauthorizedAccessException>(() => rk.DeleteSubKeyTree(name, throwOnMissingSubKey: false));
            }

            // Should throw if RegistryKey is closed
            Assert.Throws<ObjectDisposedException>(() =>
            {
                _testRegistryKey.Dispose();
                _testRegistryKey.DeleteSubKeyTree(name, throwOnMissingSubKey: true);
            });
        }

        [Fact]
        public void SubkeyMissingTest()
        {
            //Should NOT throw when throwOnMissing is false with subkey missing
            const string name = "Test";
            _testRegistryKey.DeleteSubKeyTree(name, throwOnMissingSubKey: false);
        }

        [Fact]
        public void SubkeyExistsTests()
        {
            const string subKeyExists = "SubkeyExists";
            const string subKeyExists2 = "SubkeyExists2";

            //throwOnMissing is true with subkey present
            using (var rk = _testRegistryKey.CreateSubKey(subKeyExists))
            {
                rk.CreateSubKey("a");
                rk.CreateSubKey("b");
                _testRegistryKey.DeleteSubKeyTree(subKeyExists, false);
            }
            //throwOnMissing is false with subkey present
            using (var rk = _testRegistryKey.CreateSubKey(subKeyExists2))
            {
                rk.CreateSubKey("a");
                rk.CreateSubKey("b");
                _testRegistryKey.DeleteSubKeyTree(subKeyExists2, true);
            }
        }
    }
}
