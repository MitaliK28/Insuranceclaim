using insuranceclaimproject.Dtos.Policy;
using insuranceclaimproject.Interfaces;
using insuranceclaimproject.Models;
using Microsoft.EntityFrameworkCore;

namespace insuranceclaimproject.Services
{
    public class PolicyService : IPolicyService
    {
        private readonly InsuranceContext _context;

        public PolicyService(InsuranceContext context)
        {
            _context = context;
        }

        public async Task<Policy?> GetPolicyByIdAsync(int policyId)
        {
            return await _context.Policies
                                 .Include(p => p.Policyholder)
                                 .FirstOrDefaultAsync(p => p.PolicyId == policyId);
        }

        public async Task<IEnumerable<Policy>> GetAllPoliciesAsync()
        {
            return await _context.Policies.Include(p => p.Policyholder).ToListAsync();
        }

        public async Task<Policy> CreatePolicyAsync(CreatePolicyDto policyData)
        {
            var policy = new Policy
            {
                PolicyNumber = policyData.PolicyNumber,
                PolicyholderId = policyData.PolicyholderId,
                CoverageAmount = policyData.CoverageAmount,
                PolicyStatus = PolicyStatus.Active,
                CreatedDate = DateTime.UtcNow
            };
            _context.Policies.Add(policy);
            await _context.SaveChangesAsync();
            return policy;
        }

        public async Task<bool> UpdatePolicyAsync(int policyId, UpdatePolicyDto policyData)
        {
            var policy = await _context.Policies.FindAsync(policyId);
            if (policy == null)
            {
                return false;
            }

            policy.CoverageAmount = policyData.CoverageAmount;
            policy.PolicyStatus = policyData.PolicyStatus;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePolicyAsync(int policyId)
        {
            var policy = await _context.Policies.FindAsync(policyId);
            if (policy == null)
            {
                return false;
            }

            _context.Policies.Remove(policy);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}