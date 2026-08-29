using System;
using System.Runtime.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Octokit.GraphQL.Model
{
    /// <summary>
    /// Represents countries or regions for billing and residence for a GitHub Sponsors profile.
    /// </summary>
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SponsorsCountryOrRegionCode
    {
        /// <summary>
        /// Afghanistan
        /// </summary>
        [EnumMember(Value = "AF")]
        Af,

        /// <summary>
        /// Åland
        /// </summary>
        [EnumMember(Value = "AX")]
        Ax,

        /// <summary>
        /// Albania
        /// </summary>
        [EnumMember(Value = "AL")]
        Al,

        /// <summary>
        /// Algeria
        /// </summary>
        [EnumMember(Value = "DZ")]
        Dz,

        /// <summary>
        /// American Samoa
        /// </summary>
        [EnumMember(Value = "AS")]
        As,

        /// <summary>
        /// Andorra
        /// </summary>
        [EnumMember(Value = "AD")]
        Ad,

        /// <summary>
        /// Angola
        /// </summary>
        [EnumMember(Value = "AO")]
        Ao,

        /// <summary>
        /// Anguilla
        /// </summary>
        [EnumMember(Value = "AI")]
        Ai,

        /// <summary>
        /// Antarctica
        /// </summary>
        [EnumMember(Value = "AQ")]
        Aq,

        /// <summary>
        /// Antigua and Barbuda
        /// </summary>
        [EnumMember(Value = "AG")]
        Ag,

        /// <summary>
        /// Argentina
        /// </summary>
        [EnumMember(Value = "AR")]
        Ar,

        /// <summary>
        /// Armenia
        /// </summary>
        [EnumMember(Value = "AM")]
        Am,

        /// <summary>
        /// Aruba
        /// </summary>
        [EnumMember(Value = "AW")]
        Aw,

        /// <summary>
        /// Australia
        /// </summary>
        [EnumMember(Value = "AU")]
        Au,

        /// <summary>
        /// Austria
        /// </summary>
        [EnumMember(Value = "AT")]
        At,

        /// <summary>
        /// Azerbaijan
        /// </summary>
        [EnumMember(Value = "AZ")]
        Az,

        /// <summary>
        /// Bahamas
        /// </summary>
        [EnumMember(Value = "BS")]
        Bs,

        /// <summary>
        /// Bahrain
        /// </summary>
        [EnumMember(Value = "BH")]
        Bh,

        /// <summary>
        /// Bangladesh
        /// </summary>
        [EnumMember(Value = "BD")]
        Bd,

        /// <summary>
        /// Barbados
        /// </summary>
        [EnumMember(Value = "BB")]
        Bb,

        /// <summary>
        /// Belarus
        /// </summary>
        [EnumMember(Value = "BY")]
        By,

        /// <summary>
        /// Belgium
        /// </summary>
        [EnumMember(Value = "BE")]
        Be,

        /// <summary>
        /// Belize
        /// </summary>
        [EnumMember(Value = "BZ")]
        Bz,

        /// <summary>
        /// Benin
        /// </summary>
        [EnumMember(Value = "BJ")]
        Bj,

        /// <summary>
        /// Bermuda
        /// </summary>
        [EnumMember(Value = "BM")]
        Bm,

        /// <summary>
        /// Bhutan
        /// </summary>
        [EnumMember(Value = "BT")]
        Bt,

        /// <summary>
        /// Bolivia
        /// </summary>
        [EnumMember(Value = "BO")]
        Bo,

        /// <summary>
        /// Bonaire, Sint Eustatius and Saba
        /// </summary>
        [EnumMember(Value = "BQ")]
        Bq,

        /// <summary>
        /// Bosnia and Herzegovina
        /// </summary>
        [EnumMember(Value = "BA")]
        Ba,

        /// <summary>
        /// Botswana
        /// </summary>
        [EnumMember(Value = "BW")]
        Bw,

        /// <summary>
        /// Bouvet Island
        /// </summary>
        [EnumMember(Value = "BV")]
        Bv,

        /// <summary>
        /// Brazil
        /// </summary>
        [EnumMember(Value = "BR")]
        Br,

        /// <summary>
        /// British Indian Ocean Territory
        /// </summary>
        [EnumMember(Value = "IO")]
        Io,

        /// <summary>
        /// Brunei Darussalam
        /// </summary>
        [EnumMember(Value = "BN")]
        Bn,

        /// <summary>
        /// Bulgaria
        /// </summary>
        [EnumMember(Value = "BG")]
        Bg,

        /// <summary>
        /// Burkina Faso
        /// </summary>
        [EnumMember(Value = "BF")]
        Bf,

        /// <summary>
        /// Burundi
        /// </summary>
        [EnumMember(Value = "BI")]
        Bi,

        /// <summary>
        /// Cambodia
        /// </summary>
        [EnumMember(Value = "KH")]
        Kh,

        /// <summary>
        /// Cameroon
        /// </summary>
        [EnumMember(Value = "CM")]
        Cm,

        /// <summary>
        /// Canada
        /// </summary>
        [EnumMember(Value = "CA")]
        Ca,

        /// <summary>
        /// Cape Verde
        /// </summary>
        [EnumMember(Value = "CV")]
        Cv,

        /// <summary>
        /// Cayman Islands
        /// </summary>
        [EnumMember(Value = "KY")]
        Ky,

        /// <summary>
        /// Central African Republic
        /// </summary>
        [EnumMember(Value = "CF")]
        Cf,

        /// <summary>
        /// Chad
        /// </summary>
        [EnumMember(Value = "TD")]
        Td,

        /// <summary>
        /// Chile
        /// </summary>
        [EnumMember(Value = "CL")]
        Cl,

        /// <summary>
        /// China
        /// </summary>
        [EnumMember(Value = "CN")]
        Cn,

        /// <summary>
        /// Christmas Island
        /// </summary>
        [EnumMember(Value = "CX")]
        Cx,

        /// <summary>
        /// Cocos (Keeling) Islands
        /// </summary>
        [EnumMember(Value = "CC")]
        Cc,

        /// <summary>
        /// Colombia
        /// </summary>
        [EnumMember(Value = "CO")]
        Co,

        /// <summary>
        /// Comoros
        /// </summary>
        [EnumMember(Value = "KM")]
        Km,

        /// <summary>
        /// Congo (Brazzaville)
        /// </summary>
        [EnumMember(Value = "CG")]
        Cg,

        /// <summary>
        /// Congo (Kinshasa)
        /// </summary>
        [EnumMember(Value = "CD")]
        Cd,

        /// <summary>
        /// Cook Islands
        /// </summary>
        [EnumMember(Value = "CK")]
        Ck,

        /// <summary>
        /// Costa Rica
        /// </summary>
        [EnumMember(Value = "CR")]
        Cr,

        /// <summary>
        /// Côte d'Ivoire
        /// </summary>
        [EnumMember(Value = "CI")]
        Ci,

        /// <summary>
        /// Croatia
        /// </summary>
        [EnumMember(Value = "HR")]
        Hr,

        /// <summary>
        /// Curaçao
        /// </summary>
        [EnumMember(Value = "CW")]
        Cw,

        /// <summary>
        /// Cyprus
        /// </summary>
        [EnumMember(Value = "CY")]
        Cy,

        /// <summary>
        /// Czech Republic
        /// </summary>
        [EnumMember(Value = "CZ")]
        Cz,

        /// <summary>
        /// Denmark
        /// </summary>
        [EnumMember(Value = "DK")]
        Dk,

        /// <summary>
        /// Djibouti
        /// </summary>
        [EnumMember(Value = "DJ")]
        Dj,

        /// <summary>
        /// Dominica
        /// </summary>
        [EnumMember(Value = "DM")]
        Dm,

        /// <summary>
        /// Dominican Republic
        /// </summary>
        [EnumMember(Value = "DO")]
        Do,

        /// <summary>
        /// Ecuador
        /// </summary>
        [EnumMember(Value = "EC")]
        Ec,

        /// <summary>
        /// Egypt
        /// </summary>
        [EnumMember(Value = "EG")]
        Eg,

        /// <summary>
        /// El Salvador
        /// </summary>
        [EnumMember(Value = "SV")]
        Sv,

        /// <summary>
        /// Equatorial Guinea
        /// </summary>
        [EnumMember(Value = "GQ")]
        Gq,

        /// <summary>
        /// Eritrea
        /// </summary>
        [EnumMember(Value = "ER")]
        Er,

        /// <summary>
        /// Estonia
        /// </summary>
        [EnumMember(Value = "EE")]
        Ee,

        /// <summary>
        /// Ethiopia
        /// </summary>
        [EnumMember(Value = "ET")]
        Et,

        /// <summary>
        /// Falkland Islands
        /// </summary>
        [EnumMember(Value = "FK")]
        Fk,

        /// <summary>
        /// Faroe Islands
        /// </summary>
        [EnumMember(Value = "FO")]
        Fo,

        /// <summary>
        /// Fiji
        /// </summary>
        [EnumMember(Value = "FJ")]
        Fj,

        /// <summary>
        /// Finland
        /// </summary>
        [EnumMember(Value = "FI")]
        Fi,

        /// <summary>
        /// France
        /// </summary>
        [EnumMember(Value = "FR")]
        Fr,

        /// <summary>
        /// French Guiana
        /// </summary>
        [EnumMember(Value = "GF")]
        Gf,

        /// <summary>
        /// French Polynesia
        /// </summary>
        [EnumMember(Value = "PF")]
        Pf,

        /// <summary>
        /// French Southern Lands
        /// </summary>
        [EnumMember(Value = "TF")]
        Tf,

        /// <summary>
        /// Gabon
        /// </summary>
        [EnumMember(Value = "GA")]
        Ga,

        /// <summary>
        /// Gambia
        /// </summary>
        [EnumMember(Value = "GM")]
        Gm,

        /// <summary>
        /// Georgia
        /// </summary>
        [EnumMember(Value = "GE")]
        Ge,

        /// <summary>
        /// Germany
        /// </summary>
        [EnumMember(Value = "DE")]
        De,

        /// <summary>
        /// Ghana
        /// </summary>
        [EnumMember(Value = "GH")]
        Gh,

        /// <summary>
        /// Gibraltar
        /// </summary>
        [EnumMember(Value = "GI")]
        Gi,

        /// <summary>
        /// Greece
        /// </summary>
        [EnumMember(Value = "GR")]
        Gr,

        /// <summary>
        /// Greenland
        /// </summary>
        [EnumMember(Value = "GL")]
        Gl,

        /// <summary>
        /// Grenada
        /// </summary>
        [EnumMember(Value = "GD")]
        Gd,

        /// <summary>
        /// Guadeloupe
        /// </summary>
        [EnumMember(Value = "GP")]
        Gp,

        /// <summary>
        /// Guam
        /// </summary>
        [EnumMember(Value = "GU")]
        Gu,

        /// <summary>
        /// Guatemala
        /// </summary>
        [EnumMember(Value = "GT")]
        Gt,

        /// <summary>
        /// Guernsey
        /// </summary>
        [EnumMember(Value = "GG")]
        Gg,

        /// <summary>
        /// Guinea
        /// </summary>
        [EnumMember(Value = "GN")]
        Gn,

        /// <summary>
        /// Guinea-Bissau
        /// </summary>
        [EnumMember(Value = "GW")]
        Gw,

        /// <summary>
        /// Guyana
        /// </summary>
        [EnumMember(Value = "GY")]
        Gy,

        /// <summary>
        /// Haiti
        /// </summary>
        [EnumMember(Value = "HT")]
        Ht,

        /// <summary>
        /// Heard and McDonald Islands
        /// </summary>
        [EnumMember(Value = "HM")]
        Hm,

        /// <summary>
        /// Honduras
        /// </summary>
        [EnumMember(Value = "HN")]
        Hn,

        /// <summary>
        /// Hong Kong
        /// </summary>
        [EnumMember(Value = "HK")]
        Hk,

        /// <summary>
        /// Hungary
        /// </summary>
        [EnumMember(Value = "HU")]
        Hu,

        /// <summary>
        /// Iceland
        /// </summary>
        [EnumMember(Value = "IS")]
        Is,

        /// <summary>
        /// India
        /// </summary>
        [EnumMember(Value = "IN")]
        In,

        /// <summary>
        /// Indonesia
        /// </summary>
        [EnumMember(Value = "ID")]
        Id,

        /// <summary>
        /// Iran
        /// </summary>
        [EnumMember(Value = "IR")]
        Ir,

        /// <summary>
        /// Iraq
        /// </summary>
        [EnumMember(Value = "IQ")]
        Iq,

        /// <summary>
        /// Ireland
        /// </summary>
        [EnumMember(Value = "IE")]
        Ie,

        /// <summary>
        /// Isle of Man
        /// </summary>
        [EnumMember(Value = "IM")]
        Im,

        /// <summary>
        /// Israel
        /// </summary>
        [EnumMember(Value = "IL")]
        Il,

        /// <summary>
        /// Italy
        /// </summary>
        [EnumMember(Value = "IT")]
        It,

        /// <summary>
        /// Jamaica
        /// </summary>
        [EnumMember(Value = "JM")]
        Jm,

        /// <summary>
        /// Japan
        /// </summary>
        [EnumMember(Value = "JP")]
        Jp,

        /// <summary>
        /// Jersey
        /// </summary>
        [EnumMember(Value = "JE")]
        Je,

        /// <summary>
        /// Jordan
        /// </summary>
        [EnumMember(Value = "JO")]
        Jo,

        /// <summary>
        /// Kazakhstan
        /// </summary>
        [EnumMember(Value = "KZ")]
        Kz,

        /// <summary>
        /// Kenya
        /// </summary>
        [EnumMember(Value = "KE")]
        Ke,

        /// <summary>
        /// Kiribati
        /// </summary>
        [EnumMember(Value = "KI")]
        Ki,

        /// <summary>
        /// Korea, South
        /// </summary>
        [EnumMember(Value = "KR")]
        Kr,

        /// <summary>
        /// Kuwait
        /// </summary>
        [EnumMember(Value = "KW")]
        Kw,

        /// <summary>
        /// Kyrgyzstan
        /// </summary>
        [EnumMember(Value = "KG")]
        Kg,

        /// <summary>
        /// Laos
        /// </summary>
        [EnumMember(Value = "LA")]
        La,

        /// <summary>
        /// Latvia
        /// </summary>
        [EnumMember(Value = "LV")]
        Lv,

        /// <summary>
        /// Lebanon
        /// </summary>
        [EnumMember(Value = "LB")]
        Lb,

        /// <summary>
        /// Lesotho
        /// </summary>
        [EnumMember(Value = "LS")]
        Ls,

        /// <summary>
        /// Liberia
        /// </summary>
        [EnumMember(Value = "LR")]
        Lr,

        /// <summary>
        /// Libya
        /// </summary>
        [EnumMember(Value = "LY")]
        Ly,

        /// <summary>
        /// Liechtenstein
        /// </summary>
        [EnumMember(Value = "LI")]
        Li,

        /// <summary>
        /// Lithuania
        /// </summary>
        [EnumMember(Value = "LT")]
        Lt,

        /// <summary>
        /// Luxembourg
        /// </summary>
        [EnumMember(Value = "LU")]
        Lu,

        /// <summary>
        /// Macau
        /// </summary>
        [EnumMember(Value = "MO")]
        Mo,

        /// <summary>
        /// Macedonia
        /// </summary>
        [EnumMember(Value = "MK")]
        Mk,

        /// <summary>
        /// Madagascar
        /// </summary>
        [EnumMember(Value = "MG")]
        Mg,

        /// <summary>
        /// Malawi
        /// </summary>
        [EnumMember(Value = "MW")]
        Mw,

        /// <summary>
        /// Malaysia
        /// </summary>
        [EnumMember(Value = "MY")]
        My,

        /// <summary>
        /// Maldives
        /// </summary>
        [EnumMember(Value = "MV")]
        Mv,

        /// <summary>
        /// Mali
        /// </summary>
        [EnumMember(Value = "ML")]
        Ml,

        /// <summary>
        /// Malta
        /// </summary>
        [EnumMember(Value = "MT")]
        Mt,

        /// <summary>
        /// Marshall Islands
        /// </summary>
        [EnumMember(Value = "MH")]
        Mh,

        /// <summary>
        /// Martinique
        /// </summary>
        [EnumMember(Value = "MQ")]
        Mq,

        /// <summary>
        /// Mauritania
        /// </summary>
        [EnumMember(Value = "MR")]
        Mr,

        /// <summary>
        /// Mauritius
        /// </summary>
        [EnumMember(Value = "MU")]
        Mu,

        /// <summary>
        /// Mayotte
        /// </summary>
        [EnumMember(Value = "YT")]
        Yt,

        /// <summary>
        /// Mexico
        /// </summary>
        [EnumMember(Value = "MX")]
        Mx,

        /// <summary>
        /// Micronesia
        /// </summary>
        [EnumMember(Value = "FM")]
        Fm,

        /// <summary>
        /// Moldova
        /// </summary>
        [EnumMember(Value = "MD")]
        Md,

        /// <summary>
        /// Monaco
        /// </summary>
        [EnumMember(Value = "MC")]
        Mc,

        /// <summary>
        /// Mongolia
        /// </summary>
        [EnumMember(Value = "MN")]
        Mn,

        /// <summary>
        /// Montenegro
        /// </summary>
        [EnumMember(Value = "ME")]
        Me,

        /// <summary>
        /// Montserrat
        /// </summary>
        [EnumMember(Value = "MS")]
        Ms,

        /// <summary>
        /// Morocco
        /// </summary>
        [EnumMember(Value = "MA")]
        Ma,

        /// <summary>
        /// Mozambique
        /// </summary>
        [EnumMember(Value = "MZ")]
        Mz,

        /// <summary>
        /// Myanmar
        /// </summary>
        [EnumMember(Value = "MM")]
        Mm,

        /// <summary>
        /// Namibia
        /// </summary>
        [EnumMember(Value = "NA")]
        Na,

        /// <summary>
        /// Nauru
        /// </summary>
        [EnumMember(Value = "NR")]
        Nr,

        /// <summary>
        /// Nepal
        /// </summary>
        [EnumMember(Value = "NP")]
        Np,

        /// <summary>
        /// Netherlands
        /// </summary>
        [EnumMember(Value = "NL")]
        Nl,

        /// <summary>
        /// New Caledonia
        /// </summary>
        [EnumMember(Value = "NC")]
        Nc,

        /// <summary>
        /// New Zealand
        /// </summary>
        [EnumMember(Value = "NZ")]
        Nz,

        /// <summary>
        /// Nicaragua
        /// </summary>
        [EnumMember(Value = "NI")]
        Ni,

        /// <summary>
        /// Niger
        /// </summary>
        [EnumMember(Value = "NE")]
        Ne,

        /// <summary>
        /// Nigeria
        /// </summary>
        [EnumMember(Value = "NG")]
        Ng,

        /// <summary>
        /// Niue
        /// </summary>
        [EnumMember(Value = "NU")]
        Nu,

        /// <summary>
        /// Norfolk Island
        /// </summary>
        [EnumMember(Value = "NF")]
        Nf,

        /// <summary>
        /// Northern Mariana Islands
        /// </summary>
        [EnumMember(Value = "MP")]
        Mp,

        /// <summary>
        /// Norway
        /// </summary>
        [EnumMember(Value = "NO")]
        No,

        /// <summary>
        /// Oman
        /// </summary>
        [EnumMember(Value = "OM")]
        Om,

        /// <summary>
        /// Pakistan
        /// </summary>
        [EnumMember(Value = "PK")]
        Pk,

        /// <summary>
        /// Palau
        /// </summary>
        [EnumMember(Value = "PW")]
        Pw,

        /// <summary>
        /// Palestine
        /// </summary>
        [EnumMember(Value = "PS")]
        Ps,

        /// <summary>
        /// Panama
        /// </summary>
        [EnumMember(Value = "PA")]
        Pa,

        /// <summary>
        /// Papua New Guinea
        /// </summary>
        [EnumMember(Value = "PG")]
        Pg,

        /// <summary>
        /// Paraguay
        /// </summary>
        [EnumMember(Value = "PY")]
        Py,

        /// <summary>
        /// Peru
        /// </summary>
        [EnumMember(Value = "PE")]
        Pe,

        /// <summary>
        /// Philippines
        /// </summary>
        [EnumMember(Value = "PH")]
        Ph,

        /// <summary>
        /// Pitcairn
        /// </summary>
        [EnumMember(Value = "PN")]
        Pn,

        /// <summary>
        /// Poland
        /// </summary>
        [EnumMember(Value = "PL")]
        Pl,

        /// <summary>
        /// Portugal
        /// </summary>
        [EnumMember(Value = "PT")]
        Pt,

        /// <summary>
        /// Puerto Rico
        /// </summary>
        [EnumMember(Value = "PR")]
        Pr,

        /// <summary>
        /// Qatar
        /// </summary>
        [EnumMember(Value = "QA")]
        Qa,

        /// <summary>
        /// Reunion
        /// </summary>
        [EnumMember(Value = "RE")]
        Re,

        /// <summary>
        /// Romania
        /// </summary>
        [EnumMember(Value = "RO")]
        Ro,

        /// <summary>
        /// Russian Federation
        /// </summary>
        [EnumMember(Value = "RU")]
        Ru,

        /// <summary>
        /// Rwanda
        /// </summary>
        [EnumMember(Value = "RW")]
        Rw,

        /// <summary>
        /// Saint Barthélemy
        /// </summary>
        [EnumMember(Value = "BL")]
        Bl,

        /// <summary>
        /// Saint Helena
        /// </summary>
        [EnumMember(Value = "SH")]
        Sh,

        /// <summary>
        /// Saint Kitts and Nevis
        /// </summary>
        [EnumMember(Value = "KN")]
        Kn,

        /// <summary>
        /// Saint Lucia
        /// </summary>
        [EnumMember(Value = "LC")]
        Lc,

        /// <summary>
        /// Saint Martin (French part)
        /// </summary>
        [EnumMember(Value = "MF")]
        Mf,

        /// <summary>
        /// Saint Pierre and Miquelon
        /// </summary>
        [EnumMember(Value = "PM")]
        Pm,

        /// <summary>
        /// Saint Vincent and the Grenadines
        /// </summary>
        [EnumMember(Value = "VC")]
        Vc,

        /// <summary>
        /// Samoa
        /// </summary>
        [EnumMember(Value = "WS")]
        Ws,

        /// <summary>
        /// San Marino
        /// </summary>
        [EnumMember(Value = "SM")]
        Sm,

        /// <summary>
        /// Sao Tome and Principe
        /// </summary>
        [EnumMember(Value = "ST")]
        St,

        /// <summary>
        /// Saudi Arabia
        /// </summary>
        [EnumMember(Value = "SA")]
        Sa,

        /// <summary>
        /// Senegal
        /// </summary>
        [EnumMember(Value = "SN")]
        Sn,

        /// <summary>
        /// Serbia
        /// </summary>
        [EnumMember(Value = "RS")]
        Rs,

        /// <summary>
        /// Seychelles
        /// </summary>
        [EnumMember(Value = "SC")]
        Sc,

        /// <summary>
        /// Sierra Leone
        /// </summary>
        [EnumMember(Value = "SL")]
        Sl,

        /// <summary>
        /// Singapore
        /// </summary>
        [EnumMember(Value = "SG")]
        Sg,

        /// <summary>
        /// Sint Maarten (Dutch part)
        /// </summary>
        [EnumMember(Value = "SX")]
        Sx,

        /// <summary>
        /// Slovakia
        /// </summary>
        [EnumMember(Value = "SK")]
        Sk,

        /// <summary>
        /// Slovenia
        /// </summary>
        [EnumMember(Value = "SI")]
        Si,

        /// <summary>
        /// Solomon Islands
        /// </summary>
        [EnumMember(Value = "SB")]
        Sb,

        /// <summary>
        /// Somalia
        /// </summary>
        [EnumMember(Value = "SO")]
        So,

        /// <summary>
        /// South Africa
        /// </summary>
        [EnumMember(Value = "ZA")]
        Za,

        /// <summary>
        /// South Georgia and South Sandwich Islands
        /// </summary>
        [EnumMember(Value = "GS")]
        Gs,

        /// <summary>
        /// South Sudan
        /// </summary>
        [EnumMember(Value = "SS")]
        Ss,

        /// <summary>
        /// Spain
        /// </summary>
        [EnumMember(Value = "ES")]
        Es,

        /// <summary>
        /// Sri Lanka
        /// </summary>
        [EnumMember(Value = "LK")]
        Lk,

        /// <summary>
        /// Sudan
        /// </summary>
        [EnumMember(Value = "SD")]
        Sd,

        /// <summary>
        /// Suriname
        /// </summary>
        [EnumMember(Value = "SR")]
        Sr,

        /// <summary>
        /// Svalbard and Jan Mayen Islands
        /// </summary>
        [EnumMember(Value = "SJ")]
        Sj,

        /// <summary>
        /// Swaziland
        /// </summary>
        [EnumMember(Value = "SZ")]
        Sz,

        /// <summary>
        /// Sweden
        /// </summary>
        [EnumMember(Value = "SE")]
        Se,

        /// <summary>
        /// Switzerland
        /// </summary>
        [EnumMember(Value = "CH")]
        Ch,

        /// <summary>
        /// Taiwan
        /// </summary>
        [EnumMember(Value = "TW")]
        Tw,

        /// <summary>
        /// Tajikistan
        /// </summary>
        [EnumMember(Value = "TJ")]
        Tj,

        /// <summary>
        /// Tanzania
        /// </summary>
        [EnumMember(Value = "TZ")]
        Tz,

        /// <summary>
        /// Thailand
        /// </summary>
        [EnumMember(Value = "TH")]
        Th,

        /// <summary>
        /// Timor-Leste
        /// </summary>
        [EnumMember(Value = "TL")]
        Tl,

        /// <summary>
        /// Togo
        /// </summary>
        [EnumMember(Value = "TG")]
        Tg,

        /// <summary>
        /// Tokelau
        /// </summary>
        [EnumMember(Value = "TK")]
        Tk,

        /// <summary>
        /// Tonga
        /// </summary>
        [EnumMember(Value = "TO")]
        To,

        /// <summary>
        /// Trinidad and Tobago
        /// </summary>
        [EnumMember(Value = "TT")]
        Tt,

        /// <summary>
        /// Tunisia
        /// </summary>
        [EnumMember(Value = "TN")]
        Tn,

        /// <summary>
        /// Türkiye
        /// </summary>
        [EnumMember(Value = "TR")]
        Tr,

        /// <summary>
        /// Turkmenistan
        /// </summary>
        [EnumMember(Value = "TM")]
        Tm,

        /// <summary>
        /// Turks and Caicos Islands
        /// </summary>
        [EnumMember(Value = "TC")]
        Tc,

        /// <summary>
        /// Tuvalu
        /// </summary>
        [EnumMember(Value = "TV")]
        Tv,

        /// <summary>
        /// Uganda
        /// </summary>
        [EnumMember(Value = "UG")]
        Ug,

        /// <summary>
        /// Ukraine
        /// </summary>
        [EnumMember(Value = "UA")]
        Ua,

        /// <summary>
        /// United Arab Emirates
        /// </summary>
        [EnumMember(Value = "AE")]
        Ae,

        /// <summary>
        /// United Kingdom
        /// </summary>
        [EnumMember(Value = "GB")]
        Gb,

        /// <summary>
        /// United States Minor Outlying Islands
        /// </summary>
        [EnumMember(Value = "UM")]
        Um,

        /// <summary>
        /// United States of America
        /// </summary>
        [EnumMember(Value = "US")]
        Us,

        /// <summary>
        /// Uruguay
        /// </summary>
        [EnumMember(Value = "UY")]
        Uy,

        /// <summary>
        /// Uzbekistan
        /// </summary>
        [EnumMember(Value = "UZ")]
        Uz,

        /// <summary>
        /// Vanuatu
        /// </summary>
        [EnumMember(Value = "VU")]
        Vu,

        /// <summary>
        /// Vatican City
        /// </summary>
        [EnumMember(Value = "VA")]
        Va,

        /// <summary>
        /// Venezuela
        /// </summary>
        [EnumMember(Value = "VE")]
        Ve,

        /// <summary>
        /// Vietnam
        /// </summary>
        [EnumMember(Value = "VN")]
        Vn,

        /// <summary>
        /// Virgin Islands, British
        /// </summary>
        [EnumMember(Value = "VG")]
        Vg,

        /// <summary>
        /// Virgin Islands, U.S.
        /// </summary>
        [EnumMember(Value = "VI")]
        Vi,

        /// <summary>
        /// Wallis and Futuna Islands
        /// </summary>
        [EnumMember(Value = "WF")]
        Wf,

        /// <summary>
        /// Western Sahara
        /// </summary>
        [EnumMember(Value = "EH")]
        Eh,

        /// <summary>
        /// Yemen
        /// </summary>
        [EnumMember(Value = "YE")]
        Ye,

        /// <summary>
        /// Zambia
        /// </summary>
        [EnumMember(Value = "ZM")]
        Zm,

        /// <summary>
        /// Zimbabwe
        /// </summary>
        [EnumMember(Value = "ZW")]
        Zw,
    }
}