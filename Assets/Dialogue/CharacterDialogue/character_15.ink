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
Well, well, well! Gooood evening, dear receptionist!
* [>] -> section_2
* [Skip] -> section_17

== section_2 ==
Tell me, is Nocturne ready for the likes of… me? Hahaha!
* [>] -> section_3

== section_3 ==
~ talking = protagonist_name
Good evening, sir. Welcome to Nocturne.
* [>] -> section_4

== section_4 ==
May I scan your ID to confirm your booking?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Oh, ho! Booking? No reservation! I like to keep things… surprising!
* [>] -> section_6

== section_6 ==
—but, here! Confirm away, maestro!
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
Thank you, Mr. d'Cloon.
* [>] -> section_8

== section_8 ==
Could you also fill out this document with any preferences or requests?
* [>] -> section_9

== section_9 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Preferences? Oh, let me give you a show, ha... HA!
* [>] -> section_10

== section_10 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Let’s see… Two donuts, two hotdogs, and two cans of beer!
* [>] -> section_11

== section_11 ==
~ talking = visitor_name
Perfection, don’t you think? haHaHA!
* [>] -> section_12

== section_12 ==
~ talking = protagonist_name
That’s certainly a unique combination, Mr. d'Cloon.
* [>] -> section_13

== section_13 ==
~ talking = visitor_name
Unique is just fancy for crazy, right?
* [>] -> section_14

== section_14 ==
Oh.. hhhoo! I almost forgot. I’ve got an ice cream truck!
* [>] -> section_15

== section_15 ==
And I need a nice cozy spot to park my baby melting machine!
* [>] -> section_16

== section_16 ==
~ talking = protagonist_name
Absolutely, sir. I'll ask the officer to park it.
* [>] -> section_17

== section_17 ==
~ dialogue_state = "card"
—and please wait, I’ll make sure it’s prepared exactly as you requested.
* [x] -> section_18
* [x] -> section_18

== section_18 ==
~ dialogue_state = "card_given"
Your room is ready—room 606 on the sixth floor. Here’s your access card.
* [>] -> section_19

== section_19 ==
~ talking = visitor_name
haHaHA! You’re good, I like you!
* [>] -> section_20

== section_20 ==
Maybe you should come have a donut with me. Or… maybe not!
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
