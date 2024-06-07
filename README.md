# A C# project that contains obsolete encryption/hashing/serialization methods.
**None of the methods should be used in a real application**

## Why does anyone need obsolete encryption?
To evaluate code analysis tools, I wanted some code with known issues.

## There are 3 main classes in the Model project
### 1 Encryption
The ObsoleteEncryptor class here will encrypt and decrypt with an obsolete Rijndael algorithm.

### 2 Hashing
The ObsoleteHasher class here will hash with the obsolete MD5 algorithm.

### 3 Serialization
The ObsoleteSerializer class here will serialize with a Json framework with a known security vulnerability.
