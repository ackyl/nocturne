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
Hi, good evening!
* [>] -> section_2
* [Skip] -> section_13

== section_2 ==
~ talking = protagonist_name
Good evening, welcome to Nocturne Hotel. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Of course, here you go.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you, Ms. Smith. It's a pleasure to have you here. How's your day been so far?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
~ dialogue_state = "coachmark"
It’s been amazing, today is my eleventh day traveling around here!
* [>] -> section_6

== section_6 ==
~ talking = protagonist_name
That’s wonderful to hear! Exploring anything in particular, or just going where the adventure takes you?
* [>] -> section_7

== section_7 ==
~ talking = visitor_name
A mix of both, really. I love just soaking in the atmosphere and finding hidden gems.
* [>] -> section_8

== section_8 ==
~ talking = protagonist_name
Sounds like a great way to travel. By the way, I’ll need you to fill out this document.
* [>] -> section_9

== section_9 ==
~ talking = protagonist_name
Just your identity confirmation and any specific hotel requirements.
* [>] -> section_10

== section_10 ==
~ talking = visitor_name
Sure thing, thank you.
* [>] -> section_11

== section_11 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Here it is.
* [>] -> section_12

== section_12 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Perfect, thank you. I see here you’ve requested a smoking room.
* [>] -> section_13

== section_13 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Let me check the availability for you.
* [x] -> section_14
* [x] -> section_14

== section_14 ==
~ dialogue_state = "card_given"
~ talking = protagonist_name
You’re all set, Ms. Smith. Here’s your room access card.
* [>] -> section_15

== section_15 ==
~ talking = protagonist_name
You’ll be on the second floor in room 205. Enjoy your stay.
* [>] -> section_16

== section_16 ==
~ talking = visitor_name
Thank you so much! Have a great evening!
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
