﻿using System;

namespace KRLab.Core.DataStructures.Graphs
{
    /// <summary>
    /// This interface should be implemented alongside the IGraph interface.
    /// </summary>
    public interface IWeightedGraph<T> where T : IComparable<T>
    {
        /// <summary>
        /// Connects two vertices together with a weight, in the direction: first->second.
        /// </summary>
        bool AddEdge(T source, T destination, CWeight weight);

        /// <summary>
        /// Updates the edge weight from source to destination.
        /// </summary>
        bool UpdateEdgeWeight(T source, T destination, CWeight weight);

        /// <summary>
        /// Get edge object from source to destination.
        /// </summary>
        WeightedEdge<T> GetEdge(T source, T destination);

        /// <summary>
        /// Returns the edge weight from source to destination.
        /// </summary>
        CWeight GetEdgeWeight(T source, T destination);

        /// <summary>
        /// Returns the neighbours of a vertex as a dictionary of nodes-to-weights.
        /// </summary>
        System.Collections.Generic.Dictionary<T, CWeight> NeighboursMap(T vertex);
    }
}

