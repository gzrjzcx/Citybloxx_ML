git clone --recursive https://github.com/gzrjzcx/Citybloxx_ML
cd Citybloxx_ML
git checkout dev
git submodule update
cd Assets/CBX_RL
mkdir TO_DEL
mv * TO_DEL
mv TO_DEL/Assets .
rm -rf TO_DEL
cd Assets
mkdir TO_DEL
mv * TO_DEL
mv TO_DEL/ML_Agents .
rm -rf TO_DEL
cd ..
cd ..