# Lesson Plan: RSA Fundamentals & The Hybrid Approach

### 1. Overview
This lesson explores the practical implementation of the RSA algorithm, its mathematical boundaries, and why modern systems use a "Hybrid" approach for long messages.

---

### 2. Reading Assignment
* **Primary Resource:** [The RSA Algorithm Explained](https://doctrina.org/How-RSA-Works-With-Examples.html) – A deep dive into the relationship between the public exponent ($e$), the private exponent ($d$), and the modulus ($n$).
* **Alternative:** *Introduction to Algorithms (CLRS)*, Chapter 31.7: The RSA Cryptosystem.

---

### 3. Core Concept: The "Packing" Limit
In RSA, your message ($m$) must be treated as a single large integer. A fundamental rule of the math is:
$$m < n$$
Where $n$ is the **Modulus**. 

* **The Constraint:** If you have a 2048-bit RSA key, your message cannot exceed 2048 bits (256 bytes). 
* **The Problem:** Attempting to "pack" a long message (like a 1MB file) into a single integer results in a value much larger than $n$. Upon decryption, the modular arithmetic "wraps around," resulting in data corruption.

---

### 4. Lab Activity: Exploring [RSATwo](https://github.com/LandSharkFive/RSATwo)
**Objective:** Use the RSATwo codebase to understand key constraints and hybrid logic.

1. **Analyze the Modulus:** Locate the bit-length of the RSA keys generated in the project. Calculate the maximum number of characters you can "pack" before hitting the $n$ limit.
2. **The Hybrid Challenge:** * Generate a random 32-byte string (simulating an AES-256 key).
   * Use the **RSATwo** implementation to encrypt this 32-byte key.
   * **Discussion:** Why is it safer and faster to encrypt this 32-byte key with RSA rather than the entire message?

---

### 5. Summary: The "Digital Envelope"
Because RSA is computationally expensive and size-restricted, we use it as an **envelope**:
1. Encrypt the **Long Message** using a fast symmetric algorithm (like AES).
2. Encrypt the **Symmetric Key** using **RSA**.
3. Prepend the encrypted key to the encrypted message.

---

### 6. Discussion Questions
* How does **OAEP Padding** protect against "dictionary attacks" on the packed integer?
* Why does **ECC (Elliptic Curve Cryptography)** offer similar security to RSA but with much smaller keys?














