﻿using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Stratis.SmartContracts.Core;
using Stratis.SmartContracts.Core.Compilation;
using Xunit;

namespace Stratis.Bitcoin.Features.SmartContracts.Tests
{
    public class SmartContractCompilerTests
    {
        [Fact]
        public void SmartContract_Compiler_ReturnsFalse()
        {
            SmartContractCompilationResult compilationResult = SmartContractCompiler.Compile("Uncompilable");

            Assert.False(compilationResult.Success);
            Assert.NotEmpty(compilationResult.Diagnostics);
            Assert.Null(compilationResult.Compilation);
        }

        [Fact]
        public void SmartContract_Compiler_ReturnsTrue()
        {
            SmartContractCompilationResult compilationResult = SmartContractCompiler.Compile("class C{static void M(){}}");

            Assert.True(compilationResult.Success);
            Assert.Empty(compilationResult.Diagnostics);
            Assert.NotNull(compilationResult.Compilation);
        }

        [Fact]
        public void SmartContract_ReferenceResolver_HasCorrectAssemblies()
        {
            List<Assembly> allowedAssemblies = ReferencedAssemblyResolver.AllowedAssemblies.ToList();

            Assert.Equal(4, allowedAssemblies.Count);
            Assert.Contains(allowedAssemblies, a => a.GetName().Name == "System.Runtime");
            Assert.Contains(allowedAssemblies, a => a.GetName().Name == "System.Private.CoreLib");
            Assert.Contains(allowedAssemblies, a => a.GetName().Name == "Stratis.SmartContracts");
            Assert.Contains(allowedAssemblies, a => a.GetName().Name == "System.Linq");
        }
    }
}
