using System;

namespace MintProgram;

public abstract record Currency(string Name) { }

public record USDollar() : Currency("US Dollar") { }
