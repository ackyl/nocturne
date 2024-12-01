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
Good evening. I need a room, but I don’t have a reservation.
* [>] -> section_2
* [Skip] -> section_10

== section_2 ==
~ talking = protagonist_name
Good evening, sir. Welcome to Nocturne. May I scan your ID to confirm your details?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Sure, here you go.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Mr. Park. Could you please fill out this document with your preferences or requests?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
A forms? This is not a newspaper era anymore, I guessed.
* [>] -> section_6

== section_6 ==
But alright, let me think…
* [>] -> section_7

== section_7 ==
~ dialogue_state = "get_document"
Yeah, roasted sunflower seeds and soda would be great.
* [>] -> section_8

== section_8 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Roasted sunflower seeds and soda—unique choice.
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
Yeah, I’ve always liked them. They’re simple but perfect.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "card"
~ talking = protagonist_name
I’ll make sure it’s prepared for you right away.
* [x] -> section_11
* [x] -> section_11

== section_11 ==
~ dialogue_state = "card_given"
Your room is ready—room 404 on the fourth floor. Here’s your access card.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Thanks. I appreciate it.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
