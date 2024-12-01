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
Hi there! I’m Roxeanne. I have a reservation here.
* [>] -> section_2
* [Skip] -> section_10

== section_2 ==
~ talking = protagonist_name
Good evening, Ms. Roxeanne. Welcome to Nocturne. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Sure thing.
* [>] -> section_4

== section_4 ==
~ dialogue_state = "get_document"
~ talking = protagonist_name
Thank you. Let’s see…
* [>] -> section_5

== section_5 ==
~ dialogue_state = "finish_document"
—a seven-day stay in a deluxe room with a queen-size bed. Does that sound right?
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
That’s perfect! I just wanted something cozy to relax after my tour here.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
Sounds like you’ve been having a great time.
* [>] -> section_8

== section_8 ==
Would you like to add some specific preferences or requests for your stay?
* [>] -> section_9

== section_9 ==
~ talking = visitor_name
Uhm, no I guess, I'll call you if there are any.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Of course, it's a pleasure to serve you. Wait a moment please.
* [x] -> section_11
* [x] -> section_11

== section_11 ==
~ dialogue_state = "card_given"
Your room is ready—room 816 on the eighth floor. Here’s your access card.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Thank you so much! This is already feeling like the perfect getaway.
* [>] -> section_13

== section_13 ==
~ talking = protagonist_name
I’m glad to hear that. Enjoy your stay, Ms. Roxeanne!
* [>] -> section_14

== section_14 ==
~ talking = visitor_name
Thanks! Have a great evening!
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
