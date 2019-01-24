#!/bin/bash
cp README.md text.md
# ---------------------- replace head ----------------------
h5="h5. "
h4="h4. "
h3="h3. "
h2="h2. "
h1="h1. "

sed -i "s/^#####\( \)/${h5}/" text.md
sed -i "s/^####\( \)/${h4}/" text.md
sed -i "s/^###\( \)/${h3}/" text.md
sed -i "s/^##\( \)/${h2}/" text.md
sed -i "/${h2}/a ----" text.md
sed -i "s/^#\( \)/${h1}/" text.md
sed -i "/${h1}/a ----" text.md

# ---------------------- replace list ----------------------
l2="-- "
l3="--- "
l4="---- "

sed -i "s/			- /${l4}/" text.md
sed -i "s/		- /${l3}/" text.md
sed -i "s/	- /${l2}/" text.md

# ---------------------- replace bold ----------------------
sed -i "s/\`/\*/g" text.md
sed -i "s/\*\*/\*/g" text.md

# ---------------------- replace link ----------------------
sed -i "/\:\/\//{ s/)/\]/ }" text.md
sed -i "/\:\/\//{ s/\](/\|/ }" text.md

# ---------------------- not modify below ----------------------
cat text.md | pbcopy
rm -f text.md

# Because there may be one or more # in one line, like ### or #####
# we need to substitute the # according to the order from most to least
# to avoid the wrong substitutation like ####h1. 













