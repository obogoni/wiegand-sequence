I wanted to manage access code sequences in a way that would conform with the Wiegand 26-bit format limitations. This would allow me to increase the number of possible access codes that could be used on access turnstiles designed to use the Wiegand 26-bit format for access cards.

This is the format:

![image](https://github.com/user-attachments/assets/1ba9a9fc-70a1-4024-a76f-79a0fa138def)

This format represents a number where the first 3 digits (facility code) represent an 8-bit value and the last 5 digits (card number) represent a 16-bit value. This gives a total of 65,535 cards for each facility. 

By joining the facility code with the card number as a single access code, I can have a total of 16,711,425 (255 x 65,535) identities. So I wrote this to facilitate the generation of new card numbers from a sequential sequence:

```c#

//Creates a sequence based on the passed range
var sequence = Sequence.From(10065535, 25565535);

//Creates the next card in the sequence
var card = sequence.Next().Value;

//Normally the next access code in the sequence would be "10065536".
//But the result is actually "10100000".
var accessCode = card.ToLong();

```

 Now I can increment the sequence to automatically generate new identities while still conforming to a wiegand format :)