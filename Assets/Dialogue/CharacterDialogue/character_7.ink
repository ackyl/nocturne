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
Good evening. I’d like to book a room if you have availability.
* [>] -> section_2
* [Skip] -> section_12

== section_2 ==
~ talking = protagonist_name
Good evening, sir. Welcome to Nocturne. May I scan your ID to confirm your details?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Of course. This place feels... serene. A rare find.
* [>] -> section_4

== section_4 ==
Ah ... I know ... the lobby smells amazing, it’s like a rose garden in here.
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
Thank you, sir. I’m glad you like it.
* [>] -> section_6

== section_6 ==
Could you please fill out this document with your preferences and requests for your stay?
* [>] -> section_7

== section_7 ==
~ talking = visitor_name
Sure.
* [>] -> section_8

== section_8 ==
~ dialogue_state = "get_document"
Here you go.
* [>] -> section_9

== section_9 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Thank you. I see you’ve requested french fries, crab cake, and soda.
* [>] -> section_10

== section_10 ==
May I suggest one of our packages? It includes french fries, chicken wings, and soda.
* [>] -> section_11

== section_11 ==
~ talking = visitor_name
Sounds good. Could you add the spicy sauce too?
* [>] -> section_12

== section_12 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Of course. Let me finalize your booking.
* [x] -> section_13
* [x] -> section_13

== section_13 ==
~ dialogue_state = "card_given"
Your room is ready—room 307 on the third floor. Here’s your access card. Enjoy your stay.
* [>] -> section_14

== section_14 ==
~ talking = visitor_name
Thanks. Seems like I made the right choice coming here.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
