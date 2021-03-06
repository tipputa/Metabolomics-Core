﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Metabolomics.MsLima.Bean;
using Metabolomics.Core.Utility;
using Metabolomics.MsLima;
using System.IO;

namespace Metabolomics.MsLima.Model
{
    public static class CompoundGroupUtility
    {
        public static List<CompoundBean> CreateCompoundList(List<MassSpectrum> rawLibraryFile, CompoundGroupingKey key)
        {
            if(key == CompoundGroupingKey.ShortInChIKey)
            {
                return CreateCompoundListByShortInChIKey(rawLibraryFile);
            }
            else if(key == CompoundGroupingKey.InChIKey)
            {
                return CreateCompoundListByInChIKey(rawLibraryFile);
            }
            else if (key == CompoundGroupingKey.InChI)
            {
                return CreateCompoundListByInChI(rawLibraryFile);
            }
            else if(key == CompoundGroupingKey.None)
            {
                return CreateCompoundListByNone(rawLibraryFile);

            }
            else 
            {
                return null;
            }

        }

        public static List<CompoundBean> CreateCompoundListByNone(List<MassSpectrum> rawLibraryFile)
        {
            if (rawLibraryFile == null || rawLibraryFile.Count == 0)
                return new List<CompoundBean>();
            var counter = 0;
            List<CompoundBean> comps = new List<CompoundBean>();
            CompoundBean comp;
            foreach (var spectrum in rawLibraryFile)
            {
                comp = new CompoundBean
                {
                    Id = counter,
                    InChIKey = spectrum.InChIKey,
                    InChI = spectrum.InChI
                };
                comp.NumSpectra++;
                comp.Spectra.Add(spectrum);
                comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                comp.Name = spectrum.Name;
                comp.Formula = spectrum.Formula;
                comp.Smiles = spectrum.Smiles;
                counter++;
                comps.Add(comp);
            }
            return comps;

        }
        public static List<CompoundBean> CreateCompoundListByShortInChIKey(List<MassSpectrum> rawLibraryFile)
        {
            if (rawLibraryFile == null || rawLibraryFile.Count == 0)
                return new List<CompoundBean>();

            CompoundBean comp;
            var dic = new Dictionary<string, CompoundBean>();
            var InchiKeys = new List<string>();

            var counter = 1;
            foreach (var spectrum in rawLibraryFile)
            {
                if (string.IsNullOrEmpty(spectrum.InChIKey))
                {
                    comp = new CompoundBean
                    {
                        Id = counter,
                        InChIKey = "",
                        InChI = spectrum.InChI
                    };
                    comp.NumSpectra++;
                    comp.Spectra.Add(spectrum);
                    comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                    comp.Name = spectrum.Name;
                    comp.Formula = spectrum.Formula;
                    comp.Smiles = spectrum.Smiles;
                    dic.Add(counter.ToString(), comp);
                    counter++;
                }
                else
                {
                    var Inchikey = spectrum.InChIKey.Split('-')[0];
                    if (InchiKeys.Contains(Inchikey))
                    {
                        dic[Inchikey].Spectra.Add(spectrum);
                        dic[Inchikey].NumSpectra++;
                    }
                    else
                    {
                        comp = new CompoundBean
                        {
                            Id = counter,
                            InChIKey = spectrum.InChIKey,
                            InChI = spectrum.InChI
                        };
                        comp.NumSpectra++;
                        comp.Spectra.Add(spectrum);
                        comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                        comp.Name = spectrum.Name;
                        comp.Formula = spectrum.Formula;
                        comp.Smiles = spectrum.Smiles;
                        dic.Add(Inchikey, comp);
                        InchiKeys.Add(Inchikey);
                        counter++;
                    }
                }
            }
            return new List<CompoundBean>(dic.Values);
        }


        public static List<CompoundBean> CreateCompoundListByInChIKey(List<MassSpectrum> rawLibraryFile)
        {
            CompoundBean comp;
            var dic = new Dictionary<string, CompoundBean>();
            var InchiKeys = new List<string>();

            var counter = 1;
            foreach (var spectrum in rawLibraryFile)
            {
                if (string.IsNullOrEmpty(spectrum.InChIKey))
                {
                    comp = new CompoundBean
                    {
                        Id = counter,
                        InChIKey = "",
                        InChI = spectrum.InChI
                    };
                    comp.NumSpectra++;
                    comp.Spectra.Add(spectrum);
                    comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                    comp.Name = spectrum.Name;
                    comp.Formula = spectrum.Formula;
                    comp.Smiles = spectrum.Smiles;
                    dic.Add(counter.ToString(), comp);
                    counter++;
                }
                else
                {
                    var InChIKey = spectrum.InChIKey;
                    if (InchiKeys.Contains(InChIKey))
                    {
                        dic[InChIKey].Spectra.Add(spectrum);
                        dic[InChIKey].NumSpectra++;
                    }
                    else
                    {
                        comp = new CompoundBean
                        {
                            Id = counter,
                            InChIKey = spectrum.InChIKey,
                            InChI = spectrum.InChI
                        };
                        comp.NumSpectra++;
                        comp.Spectra.Add(spectrum);
                        comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                        comp.Name = spectrum.Name;
                        comp.Formula = spectrum.Formula;
                        comp.Smiles = spectrum.Smiles;
                        dic.Add(InChIKey, comp);
                        InchiKeys.Add(InChIKey);
                        counter++;
                    }
                }
            }
            return new List<CompoundBean>(dic.Values);
        }


        public static List<CompoundBean> CreateCompoundListByInChI(List<MassSpectrum> rawLibraryFile)
        {
            CompoundBean comp;
            var dic = new Dictionary<string, CompoundBean>();
            var InChIs = new List<string>();

            var counter = 1;
            foreach (var spectrum in rawLibraryFile)
            {
                if (string.IsNullOrEmpty(spectrum.InChI))
                {
                    comp = new CompoundBean
                    {
                        Id = counter,
                        InChIKey = spectrum.InChIKey,
                        InChI = ""
                    };
                    comp.NumSpectra++;
                    comp.Spectra.Add(spectrum);
                    comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                    comp.Name = spectrum.Name;
                    comp.Formula = spectrum.Formula;
                    comp.Smiles = spectrum.Smiles;
                    dic.Add(counter.ToString(), comp);
                    counter++;
                }
                else
                {
                    var InChI = spectrum.InChI;
                    if (InChIs.Contains(InChI))
                    {
                        dic[InChI].Spectra.Add(spectrum);
                        dic[InChI].NumSpectra++;
                    }
                    else
                    {
                        comp = new CompoundBean
                        {
                            Id = counter,
                            InChIKey = spectrum.InChIKey,
                            InChI = spectrum.InChI
                        };
                        comp.NumSpectra++;
                        comp.Spectra.Add(spectrum);
                        comp.MolecularWeight = FormulaUtility.GetMass(spectrum.Formula);
                        comp.Name = spectrum.Name;
                        comp.Formula = spectrum.Formula;
                        comp.Smiles = spectrum.Smiles;
                        dic.Add(InChI, comp);
                        InChIs.Add(InChI);
                        counter++;
                    }
                }
            }
            return new List<CompoundBean>(dic.Values);
        }

        public static void CheckCompoundList(List<CompoundBean> compounds, float minRtDiff, ref string rtString, ref string formulaString, ref string InChIKeyString)
        {            
            var missFormula = new List<string>();
            var missInChIKeys = new List<string>();
            var missRts = new List<string>();
            foreach (var compound in compounds)
            {
                var formulas = compound.Spectra.Select(x => x.Formula).Distinct().ToList();
                if (formulas.Count > 1)
                {
                    var str = "ID: " + compound.Id + ", Name: " + compound.Name + ", Formulas: ";
                    foreach (var f in formulas) { str = str + ", " + f; }
                    missFormula.Add(str);
                }

                var InChIKey = compound.Spectra.Select(x => x.InChIKey).Distinct().ToList();
                if (InChIKey.Count > 1)
                {
                    var counter2 = 0;
                    foreach (var i in InChIKey)
                    {
                        if (!i.Contains("-UHFFFAOYSA -")) counter2++;
                    }
                    if (counter2 > 1)
                    {
                        var str = "ID: " + compound.Id + ", Name: " + compound.Name + ", InChIKeys: ";
                        foreach (var s in InChIKey) { str = str + ", " + s; }
                        missInChIKeys.Add(str);
                    }
                }
                var rtOriginal = compound.Spectra.Select(x => x.RetentionTime);
                var rts = new List<float>();
                foreach (var item in rtOriginal)
                {
                    rts.Add((float)Math.Round(item, 2));
                }
                rts = rts.Distinct().ToList();
                string str2 = "";
                var maxRt = -1f; var minRt = 100000f;
                for (var i = 0; i < rts.Count; i++)
                {
                    if (rts[i] < 0) continue;
                    if (maxRt < rts[i])
                    {
                        maxRt = rts[i];
                    }
                    if (rts[i] < minRt)
                    {
                        minRt = rts[i];
                    }
                }
                if (minRt < 100000f && maxRt > -1f && minRt < maxRt && (maxRt - minRt) > minRtDiff)
                {
                    str2 = "ID: " + compound.Id + ", Name: " + compound.Name + ", first RT: " + rts[0] + ", minRT: " + minRt + ", maxRT: " + maxRt + ", diff: " + (maxRt - minRt);
                    missRts.Add(str2);
                }
            }
            if (missRts.Count > 0)
            {
                rtString = string.Join("\n", missRts);
            }
            if (missFormula.Count > 0)
            {
                formulaString = string.Join("\n", missFormula);
            }
            if (missInChIKeys.Count > 0)
            {
                InChIKeyString = string.Join("\n", missInChIKeys);
            }
        }

        public static void ConvertActualMassToTheoreticalMass(List<CompoundBean> compounds)
        {
            foreach (var comp in compounds) {
                foreach (var spec in comp.Spectra) {
                    MassSpectrumUtility.ConvertActualMassToTheoreticalMass(spec);
                }
            }
        }

        public static void DropRetentionTime(List<CompoundBean> compounds)
        {
            foreach (var comp in compounds)
            {
                foreach (var spec in comp.Spectra)
                {
                    MassSpectrumUtility.DropRetentionTime(spec);
                }
            }
        }

        public static void RemoveUnannotatedPeaks(List<CompoundBean> compounds)
        {
            foreach (var comp in compounds)
            {
                foreach (var spec in comp.Spectra)
                {
                    MassSpectrumUtility.RemoveUnannotatedPeaks(spec);
                }
            }
        }

        public static void UpdateMetaData(List<CompoundBean> compounds, List<TemporaryFile> temporaryInfo, out List<string> errorMessage)
        {
            var inChIKeyDict = new Dictionary<string, TemporaryFile>();
            errorMessage = new List<string>();

            foreach (var row in temporaryInfo)
            {
                inChIKeyDict.Add(row.InChIKey, row);
            }
            foreach (var comp in compounds)
            {
                if (inChIKeyDict.Keys.Contains(comp.InChIKey))
                {
                    comp.InChI = inChIKeyDict[comp.InChIKey].InChI;
                    comp.Smiles = inChIKeyDict[comp.InChIKey].SMILES;
                    foreach (var spectrum in comp.Spectra)
                    {
                        if (spectrum.InChIKey == comp.InChIKey)
                        {
                            spectrum.InChI = inChIKeyDict[comp.InChIKey].InChI;
                            spectrum.Smiles = inChIKeyDict[comp.InChIKey].SMILES;
                        }
                        else if (inChIKeyDict.Keys.Contains(spectrum.InChIKey))
                        {
                            spectrum.InChI = inChIKeyDict[spectrum.InChIKey].InChI;
                            spectrum.Smiles = inChIKeyDict[spectrum.InChIKey].SMILES;
                        }
                        else if (spectrum.InChIKey.Contains("-UHFFFAOYSA-"))
                        {
                            spectrum.InChI = inChIKeyDict[comp.InChIKey].InChI;
                            spectrum.Smiles = inChIKeyDict[comp.InChIKey].SMILES;
                        }
                        else
                        {
                            errorMessage.Add("Spectrum ID: " + spectrum.Id + ": " + spectrum.InChIKey + ", " + spectrum.Name + "\n");
                        }
                    }
                }
                else
                {
                    errorMessage.Add("Componud ID: " + comp.Id + ": " + comp.InChIKey + ", " + comp.Name + "\n");
                }
            }            
        }
    }
}