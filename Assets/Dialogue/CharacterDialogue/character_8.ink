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
Hello. I’d like to book a room.
* [>] -> section_2
* [Skip] -> section_13

== section_2 ==
~ talking = protagonist_name
Sure, sir. Welcome to Nocturne.
* [>] -> section_3

== section_3 ==
May I scan your ID to confirm your details?
* [>] -> section_4

== section_4 ==
~ talking = visitor_name
Of course.
* [>] -> section_5

== section_5 ==
~ talking = protagonist_name
Thank you, Mr. Castor. Could you please fill out this document with your preferences and requests for your stay?
* [>] -> section_6

== section_6 ==
~ talking = visitor_name
Sure .. By the way, I want to celebrate my birthday tomorrow with my bro.
* [>] -> section_7

== section_7 ==
~ talking = protagonist_name
Can you recommend the best pubs nearby?
* [>] -> section_8

== section_8 ==
Happy early birthday, Mr. Castor.
* [>] -> section_9

== section_9 ==
If you’re looking for a celebration, our hotel lounge offers a vibrant atmosphere perfect for making memories.
* [>] -> section_10

== section_10 ==
~ dialogue_state = "get_document"
~ talking = visitor_name
Nice ... Oh I almost forgot, here is the document.
* [>] -> section_11

== section_11 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
Risotto, omelette, smoothie, espresso… quite the unique combination, sir.
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Some things are not for everyone.
* [>] -> section_13

== section_13 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Absolutely sir. Let me finalize your booking.
* [x] -> section_14
* [x] -> section_16

== section_14 ==
~ dialogue_state = "card_given"
Your room is ready—room 407 on the fourth floor. Here’s your access card.
* [>] -> section_15

== section_15 ==
Our hotel lounge is open until midnight, if you want to check it out.
* [>] -> section_18

== section_16 ==
~ dialogue_state = "card_given"
Your room is ready—room 407 on the fourth floor. Here’s your access card.
* [>] -> section_17

== section_17 ==
Our hotel lounge is open until midnight, if you want to check it out.
* [>] -> section_19

== section_18 ==
~ talking = visitor_name
Interesting.
* [>] -> section_end

== section_19 ==
~ talking = visitor_name
I guess I'll have to look elsewhere too.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
