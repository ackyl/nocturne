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
Evening, kid. Need a room. No reservation.
* [>] -> section_2
* [Skip] -> section_13

== section_2 ==
~ talking = protagonist_name
Good evening, sir. Welcome to Nocturne. May I scan your ID to confirm your details?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Of course.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Ognac. Could you fill out this document with any special requests for your stay?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Special requests, huh?
* [>] -> section_6

== section_6 ==
~ dialogue_state = "get_document"
Let’s go with… twin bed with cribs and Champagne without 'ice'.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Champagne without ice—Interesting choice, sir.
* [>] -> section_8

== section_8 ==
~ talking = visitor_name
Yeah, I don't like it—'Ice' kills the Champagne.
* [>] -> section_9

== section_9 ==
~ talking = protagonist_name
Okay, we will serve you with our best Champagne.
* [>] -> section_10

== section_10 ==
~ talking = visitor_name
Nice, I guess you have taste.
* [>] -> section_11

== section_11 ==
How bout you also trait me with rum, and I'll trait you next time, gin no ice.
* [>] -> section_12

== section_12 ==
~ talking = protagonist_name
That’s an interesting deal. I’ll see what I can do.
* [>] -> section_13

== section_13 ==
~ dialogue_state = "card"
~ talking = protagonist_name
—and let me prepare your room, sir.
* [x] -> section_14
* [x] -> section_14a

== section_14 ==
~ dialogue_state = "card_given"
Your room is ready—room 510 on the fifth floor. Here’s your access card.
* [>] -> section_15

== section_14a ==
~ dialogue_state = "card_given"
Your room is ready—room 510 on the fifth floor. Here’s your access card.
* [>] -> section_17

== section_15 ==
~ talking = visitor_name
Thanks. I’ll settle in, but kid… keep your head up.
* [>] -> section_16

== section_16 ==
The air’s colder than you think.
* [>] -> section_end

== section_17 ==
~ talking = visitor_name
Thanks, I guess ... I think I'll enjoy it by myself tonight.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
