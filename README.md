# PasswordGenerator.Exact
Crypro-secure random password generator that will generate the exact specified number of digits, special characters, and letters.

## Examples

* A password that's 10 characters long with 2 digits:
```csharp
var pwd = PasswordGenerator.GeneratePassword(10, 2, 0);
```

* A password that's 10 characters long with 2 digits and 2 special characters:
```csharp
var pwd = PasswordGenerator.GeneratePassword(10, 2, 2);
```

* A password that's 10 characters long with 2 digits and 2 special characters from a pre-defined list:
```csharp
var pwd = PasswordGenerator.GeneratePassword(10, 2, 2, new char[] { '!', '@', '#', '$', '%' });
```
