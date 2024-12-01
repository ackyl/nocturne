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
Evening ...
* [>] -> section_2
* [Skip] -> section_13

== section_2 ==
~ talking = protagonist_name
Good evening, sir. welcome to Nocturne. May I scan your ID to confirm your booking?
* [>] -> section_3

== section_3 ==
~ talking = visitor_name
Yeah, sure.
* [>] -> section_4

== section_4 ==
~ talking = protagonist_name
Thank you. Could you fill out this document with any preferences for your stay?
* [>] -> section_5

== section_5 ==
~ talking = visitor_name
Sure.
* [>] -> section_6

== section_6 ==
~ dialogue_state = "get_document"
Here, I’d like a mat for meditation—something to lie back on—and a rosemary cocktail. I need some kind of relaxation.
* [>] -> section_7

== section_7 ==
~ dialogue_state = "finish_document"
~ talking = protagonist_name
What kind of relaxation do you need, sir?
* [>] -> section_8

== section_8 ==
~ talking = visitor_name
Anything nostalgic.
* [>] -> section_9

== section_9 ==
My sister used to talk about this place, we promised we’d come here together someday, but… she passed away from cancer last week. So, I’m here to keep that promise.
* [>] -> section_10

== section_10 ==
~ talking = protagonist_name
I’m really sorry to hear that. That’s a meaningful way to honor her.
* [>] -> section_11

== section_11 ==
May I suggest you a room rose-scented rooms?
* [>] -> section_12

== section_12 ==
~ talking = visitor_name
Of course, it will go very well with rosemary cocktail.
* [>] -> section_13

== section_13 ==
~ dialogue_state = "card"
~ talking = protagonist_name
Let me prepare your room, wait a moment, please.
* [x] -> section_14
* [x] -> section_15

== section_14 ==
~ dialogue_state = "card_given"
Your room is all set—room 706 on the seventh floor. Here’s your key. If there’s anything else, just let us know.
* [>] -> section_16

== section_15 ==
~ dialogue_state = "card_given"
Your room is all set—room 706 on the seventh floor. Here’s your key. If there’s anything else, just let us know.
* [>] -> section_17

== section_16 ==
~ talking = visitor_name
Thanks, my sister always said this place was sharp. Glad to see she wasn’t wrong.
* [>] -> section_end

== section_17 ==
~ talking = visitor_name
My sister always said this place was sharp. Guess, that’s not always true.
* [>] -> section_end

// DIALOGUE STATE = END
== section_end ==
~ dialogue_state = "end"
-> END
