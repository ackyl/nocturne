// DEFINE VARIABLES - ONLY CHANGE visitor_name and protagonist_name
VAR visitor_name = ""
VAR protagonist_name = ""
VAR dialogue_state = ""
VAR talking = ""

// DIALOGUE STATE = START
-> section_1

== section_1 ==
~ dialogue_state = "start"
~ talking = visitor_name
Goo…d… even…ing. I… nee—need a room. No… reserv—reservation.
* [>] -> section_2
* [Skip] -> section_10

== section_2 ==
~ talking = protagonist_name
Sir, are you okay? You look like you need some help.
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
No… help. N-not here. Jus—just… hun…gry. Nee—need food… room… or new battery…
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Understood, sir. May I scan your ID to confirm your details?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Y…yes. Qui…ckly.
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
Thank you, Mr. Parker. Could you fill out this document with any preferences?
* [>] -> section_7

== section_7 ==
Or would you like me to write it for you?
* [>] -> section_8

== section_8 ==
~ talking = visitor_name
I… ca—can… do it. Nee—need… burn…ger… sunny side… egg…
* [>] -> section_9

== section_9 ==
a-and room… ground… floor.
* [>] -> section_9a

== section_9a ==
~ dialogue_state = "get_document"
Here....
* [>] -> section_9b

== section_9b ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Got it.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "card"
~ talking = protagonist_name
I’ll get everything sorted immediately.
* [x] -> section_11
* [x] -> section_11

== section_11 ==
~ dialogue_state = "card_given"
~ talking = protagonist_name
Your room is ready—room 002 on the ground floor.
* [>] -> section_12

== section_12 ==
~ talking = protagonist_name
Here’s your access card, and I’ll make sure kitchen starts your order right away.
* [>] -> section_13

== section_13 ==
~ talking = visitor_name
Th…thank… you. No… more… delay.
* [>] -> section_14

== section_14 ==
Burn…ger… better… fix me…
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
