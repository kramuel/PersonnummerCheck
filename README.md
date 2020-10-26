# PersonnummerCheck
Uppgift
Du ska utveckla ett program som validerar ett angivet personnummer. 


Nivå 1
Du ska utgå från att användaren anger tolv siffror så du behöver inte ha någon form av
felhantering för detta.
Programmet frågar användaren efter ett 12-siffrigt personnummer YYYYMMDDnnnc och
kontroller om det är korrekt uppbyggt enligt följande:
1. Rätt antal siffror dvs 12
2. Årtalet ska vara från 1753 till och med 2020 (Sverige bytte kalender 1753)
3. Månaden ska var giltig 1 – 12
4. Dagen ska vara giltig och kontrolleras mot månaden (se nedan)
5. Födelsenumret nnn ska vara 000 – 999
6. Kontrollera kön. Födelsenumret är udda för män och jämnt för kvinnor
(OBS! 0 betraktas i detta fall som udda dvs kvinna)
7. Programmet ska skriva ut på skärmen om personnumret är korrekt och om
personen är man eller kvinna (juridisk).
Antal dagar i månaden
Programmet ska kontrollera så att angiven dag stämmer med antal dagar per månad.
Observera att februari har normalt 28 dagar om det inte är skottår.
Skottår
Skottår bestäms enligt följande turordning:
1. Om året är jämnt delbart med 400 är det ett skottår
2. Alla andra år som är jämnt delbara med 100 är INTE skottår
3. Övriga år som är jämnt delbara med 4 är skottår
Exempel: år 1800 respektive 1900 var inte skottår medan år 2000 var det. 1904, 1908, 1964
och 1996 är exempel på skottår

Nivå 2
Samma krav som nivå 1. Men med följande tillägg:
• Kontrollsiffran ska beräknas med Luhn-algoritmen (se nedan)
• Användaren ska kunna ange personnumret med 10 eller 12 siffror
• Programmet ska ha felhantering.
(Luhn-algoritmen)
