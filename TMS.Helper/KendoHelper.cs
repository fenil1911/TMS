using Kendo.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Helper
{
    public class KendoHelper
    {
        public static string GetSqlExpression(IEnumerable<IFilterDescriptor> filters)
        {
            string sqlExpression = "";
            string compositeSqlExpression = "";
            foreach (IFilterDescriptor filter in filters)
            {
                string andClause = "";
                if (sqlExpression != string.Empty)
                    andClause = " AND ";
                if (filter is CompositeFilterDescriptor)
                {
                    compositeSqlExpression = GetSqlExpression((filter as CompositeFilterDescriptor).FilterDescriptors);
                    if (compositeSqlExpression != "")
                        sqlExpression += andClause + compositeSqlExpression;
                }
                else
                {
                    FilterDescriptor filterDesc = (FilterDescriptor)filter;
                    if (filterDesc.Value.GetType() == Type.GetType("System.Double"))
                    {
                        switch (filterDesc.Operator)
                        {
                            case FilterOperator.IsEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] = " + filterDesc.Value;
                                break;
                            case FilterOperator.IsGreaterThan:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] > " + filterDesc.Value;
                                break;
                            case FilterOperator.IsGreaterThanOrEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] >= " + filterDesc.Value;
                                break;
                            case FilterOperator.IsLessThan:
                                sqlExpression += andClause + andClause + "[" + filterDesc.Member + "] < " + filterDesc.Value;
                                break;
                            case FilterOperator.IsLessThanOrEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] <= " + filterDesc.Value;
                                break;
                            case FilterOperator.IsNotEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] <> " + filterDesc.Value;
                                break;
                            default:
                                break;
                        }
                    }
                    else if (filterDesc.Value.GetType() == Type.GetType("System.String"))
                    {
                        switch (filterDesc.Operator)
                        {
                            case FilterOperator.Contains:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] LIKE '%" + filterDesc.Value + "%'";
                                break;
                            case FilterOperator.DoesNotContain:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] NOT LIKE '%" + filterDesc.Value + "%'";
                                break;
                            case FilterOperator.EndsWith:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] LIKE '%" + filterDesc.Value + "'";
                                break;
                            case FilterOperator.StartsWith:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] LIKE '" + filterDesc.Value + "%'";
                                break;
                            case FilterOperator.IsEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] = '" + filterDesc.Value + "'";
                                break;
                            case FilterOperator.IsNotEqualTo:
                                sqlExpression += andClause + "[" + filterDesc.Member + "] <> '" + filterDesc.Value + "'";
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            return sqlExpression;
        }
    }
}
