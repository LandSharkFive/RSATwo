# RSATwo

A C# Console-Mode application demonstrating an advanced implementation of the RSA (Rivest–Shamir–Adleman) algorithm. This project builds upon the concepts in SimpleRSA, focusing on larger key sizes and more robust encryption methods.

## Description
RSATwo provides a practical look at Public Key Infrastructure (PKI). It demonstrates how to generate large prime numbers, create public/private key pairs, and perform encryption/decryption on data blocks rather than individual characters.

## Features
* **BigInteger Implementation:** Uses System.Numerics for handling massive integers.
* **Key Generation:** Automated generation of large primes and modular inverses.
* **Block Ciphering:** Demonstrates packing data into blocks for more efficient encryption.
* **Unit Tests:** Includes comprehensive tests to verify mathematical accuracy and cryptographic integrity.

## Install and Build
1.  **Requirement:** Visual Studio 2022 or later (.NET 6.0+ recommended).
2.  **Clone:** git clone https://github.com/LandSharkFive/RSATwo.git
3.  **Build:** Open the .sln file in Visual Studio and build the solution in **Release** mode.

## Usage
Run the console application to see the RSA handshake and encryption process in action:   ./RSATwo.exe

## Security Warning
Educational Use Only. This implementation is intended for learning purposes. For production environments, always use verified libraries such as System.Security.Cryptography which include protections against side-channel attacks and padding oracle vulnerabilities (e.g., OAEP).

## License
This project is licensed under the MIT License.
