# DotNetStandard_Item_Based_Collaborative_Filtering
Item-Based Collaborative Filtering 

I wanted a recommendation algorithm without using ML.Net for a simple class project but coudn't find any for .net or .net core so i made one for myself. 

It uses item based collaborative filtering to calculate similarites between two user on how they rate any products and based on their similarities predicts items 
that a user may like. 

It uses Euclidean Distance score method. 
To calculate an Euclidean score between two people, first we need to find what products they ranked in common, then for each product, calculate the difference in ranks and square it, when we sum all squares we a get similarity score,
all that is need to be done is normalize that score so that it falls between 0 and 1.

\begin{equation} d(p,q) = \sqrt{\sum_{i=1}^{n}(p_i-q_i)^2} \end{equation}

This is basically Euclidean distance between two points in n-dimensions
