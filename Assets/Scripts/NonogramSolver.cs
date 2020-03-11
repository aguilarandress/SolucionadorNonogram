using System;
using System.Collections;
using System.Collections.Generic;

namespace Solver
{
  class NonogramSolver
  {
    public static bool ResolverNonogram(int[,] tablero, List<List<List<int>>> pistas, int pistaFilaActual)
    {
      // Encontrar el primer espacio vacio
      int[] espacioVacio = EncontrarEspacioVacio(tablero);
      // Algoritmo termina aqui
      if (espacioVacio == null) return true;
      // try
      // {
      // Trabajar mientras haya suficiente espacio para la sequencia actual
      int espacioAntesDeSecuencia = 0;
      while (HayEspaciosDisponiblesEnFila(tablero, espacioVacio[0], pistas[0][espacioVacio[0]]))
      {
        // Revisar si la sequencia funciona
        bool ultimaSequenciaFila = pistas[0][espacioVacio[0]].Count - 1 == pistaFilaActual;
        if (RevisarSecuenciaValida(tablero, pistas, espacioVacio[0], espacioVacio[1] + espacioAntesDeSecuencia, espacioVacio[1],
            pistas[0][espacioVacio[0]][pistaFilaActual], ultimaSequenciaFila))
        {
          // Pintar la sequencia
          PintarSecuencia(tablero, espacioVacio[0], espacioVacio[1] + espacioAntesDeSecuencia,
          pistas[0][espacioVacio[0]][pistaFilaActual], 1);
          // Como pintar los espacios despues de la sequencia
          int columnaInicial = espacioVacio[1] + espacioAntesDeSecuencia + pistas[0][espacioVacio[0]][pistaFilaActual];
          if (ultimaSequenciaFila)
          {
            if (columnaInicial != tablero.GetLength(1))
              PintarSecuencia(tablero, espacioVacio[0], columnaInicial, tablero.GetLength(1) - columnaInicial, 2);
          }
          else
          {
            PintarSecuencia(tablero, espacioVacio[0], columnaInicial, 1, 2);
          }

          // Continuar resolviendo
          if (ResolverNonogram(tablero, pistas, ultimaSequenciaFila ? 0 : pistaFilaActual + 1))
          {
            return true;
          }
          // Regresar al estado anterior
          PintarSecuencia(tablero, espacioVacio[0], espacioVacio[1] + espacioAntesDeSecuencia,
                          tablero.GetLength(1) - (espacioVacio[1] + espacioAntesDeSecuencia), 0);
        }
        // Si la sequencia es del mismo tamanno que la fila
        if (pistas[0][espacioVacio[0]][pistaFilaActual] == tablero.GetLength(1))
          return false;
        // Mover sequencia una posicion a la derecha
        tablero[espacioVacio[0], espacioVacio[1] + espacioAntesDeSecuencia] = 2;
        espacioAntesDeSecuencia++;
      }
      // Limpiar todos los espacios de la fila
      for (int i = espacioVacio[1]; i < tablero.GetLength(1); i++)
      {
        tablero[espacioVacio[0], i] = 0;
      }
      // }
      // catch (System.IndexOutOfRangeException)
      // {
      //   Nonogram.Program.PrintMatrix(tablero);
      //   Console.WriteLine("Pista Fila actual: {0}", pistaFilaActual);
      //   Console.WriteLine("Pista Actual: {0}", espacioVacio[0]);
      // }

      return false;
    }

    static bool RevisarSecuenciaValida(int[,] tablero, List<List<List<int>>> pistas,
    int fila, int columna, int columnaOriginal, int size, bool esUltima)
    {
      // Crear copia del tablero
      int[,] tableroCopia = tablero.Clone() as int[,];
      // Pintar sequencia
      PintarSecuencia(tableroCopia, fila, columna, size, 1);
      // Revisar hasta donde se termina de pintar los espacios de la sequencia
      int indexFinalRevision;
      if (esUltima)
      {
        indexFinalRevision = tablero.GetLength(1);
        if (columna + size != tablero.GetLength(1))
          PintarSecuencia(tableroCopia, fila, columna + size, tablero.GetLength(1) - (columna + size), 2);
      }
      else
      {
        indexFinalRevision = columna + size + 1 > tablero.GetLength(1) ? columna + size : columna + size + 1;
        PintarSecuencia(tableroCopia, fila, columna + size, 1, 2);
      }
      // Probar secuencia por columnas
      for (int i = columnaOriginal; i < indexFinalRevision; i++)
      {
        List<int> sequenciasActuales = new List<int>();
        int sequenciaActual = 0;
        bool columnaTerminada = true;
        for (int j = 0; j < tableroCopia.GetLength(0); j++)
        {
          // Agregar a la sequencia actual
          if (tableroCopia[j, i] == 1) sequenciaActual++;
          // Espacio entre sequencias
          else if (tableroCopia[j, i] == 2)
          {
            if (sequenciaActual != 0)
            {
              sequenciasActuales.Add(sequenciaActual);
              sequenciaActual = 0;
            }
          }
          else if (tableroCopia[j, i] == 0)
          {
            columnaTerminada = false;
            break;
          }
        }
        // Agregar la ultima sequencia de columna
        if (columnaTerminada && sequenciaActual != 0) sequenciasActuales.Add(sequenciaActual);
        // Verificar numero de 2 y el numero sequencias en la columna
        if (sequenciasActuales.Count > pistas[1][i].Count ||
        !RevisarEspaciosColumna(tableroCopia, i, pistas[1][i]))
          return false;
        // Revisar sequencias encontradas
        for (int x = 0; x < sequenciasActuales.Count; x++)
        {
          if (sequenciasActuales[x] != pistas[1][i][x]) return false;
        }
      }
      // Revisar que esten todas las sequencias
      if (RevisarFilaCompleta(tableroCopia, fila))
      {
        int totalPistas = 0;
        for (int i = 0; i < pistas[0][fila].Count; i++)
        {
          totalPistas += pistas[0][fila][i];
        }
        int totalCasillasTablero = 0;
        for (int i = 0; i < tableroCopia.GetLength(1); i++)
        {
          if (tableroCopia[fila, i] == 1) totalCasillasTablero++;
        }
        if (totalPistas != totalCasillasTablero) return false;
      }

      return true;
    }

    static bool RevisarFilaCompleta(int[,] tablero, int fila)
    {
      for (int i = 0; i < tablero.GetLength(1); i++)
      {
        if (tablero[fila, i] == 0) return false;
      }
      return true;
    }

    static void PintarSecuencia(int[,] tablero, int fila, int columna, int size, int numero)
    {
      for (int i = 0; i < size; i++)
      {
        tablero[fila, columna] = numero;
        columna++;
      }
    }

    static int GetEspacioDisponibleFila(int[,] tablero, int fila, List<int> pistasDeFila)
    {
      // Obtener Total de secuencias
      int resultado = 0;
      for (int i = 0; i < pistasDeFila.Count; i++)
      {
        resultado += pistasDeFila[i];
      }
      return tablero.GetLength(1) - resultado;
    }

    static bool HayEspaciosDisponiblesEnFila(int[,] tablero, int fila, List<int> pistasDeFila)
    {
      int cantidadEspacios = 0;
      for (int i = 0; i < tablero.GetLength(1); i++)
      {
        if (tablero[fila, i] == 2) cantidadEspacios++;
      }
      int totalPistas = 0;
      for (int i = 0; i < pistasDeFila.Count; i++)
      {
        totalPistas += pistasDeFila[i];
      }
      return cantidadEspacios <= tablero.GetLength(1) - totalPistas;
    }

    static bool RevisarEspaciosColumna(int[,] tablero, int columna, List<int> pistasColumna)
    {
      // Obtener la cantidad de espacios
      int cantidadEspacios = 0;
      for (int i = 0; i < tablero.GetLength(0); i++)
        if (tablero[i, columna] == 2) cantidadEspacios++;
      // Obtener el total de las pistas
      int totalPistas = 0;
      for (int i = 0; i < pistasColumna.Count; i++)
        totalPistas += pistasColumna[i];
      return cantidadEspacios + totalPistas <= tablero.GetLength(0);
    }

    static int[] EncontrarEspacioVacio(int[,] matrix)
    {
      for (int i = 0; i < matrix.GetLength(0); i++)
      {
        for (int j = 0; j < matrix.GetLength(1); j++)
        {
          if (matrix[i, j] == 0) return new int[] { i, j };
        }
      }
      return null;
    }
  }
}