using AutoMapper;
using Domain.Entities;
using Repository.Repositories;
using Repository.Repositories.Interface;
using Service.DTO.Admin.BlogCategory;
using Service.DTO.Admin.Category;
using Service.DTO.Admin.Tag;
using Service.Helpers;
using Service.Services.Interfaces;

namespace Service.Services
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _tagRepository;
        private readonly IMapper _mapper;
        public TagService(ITagRepository tagRepository,
                                IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }
        public async Task CreateAsync(TagCreateDto model)
        {
            var color = await _tagRepository.GetAllWithExpressionAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (color.ToList().Count > 0) throw new ArgumentException("This tag has already exist");
            await _tagRepository.CreateAsync(_mapper.Map<Tag>(model));
        }

        public async Task DeleteAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new KeyNotFoundException($"Tag with ID {id} not found.");
            await _tagRepository.DeleteAsync(tag);

        }

        public async Task EditAsync(TagEditDto model, int id)
        {
            var color = await _tagRepository.GetAllWithExpressionAsync(x => x.Name.ToLower() == model.Name.ToLower());
            if (color.ToList().Count > 0) throw new ArgumentException("This tag has already exist");
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new KeyNotFoundException($"Tag with ID {id} not found.");
            _mapper.Map(model, tag);
            await _tagRepository.EditAsync(tag);
        }

        public async Task<IEnumerable<TagDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<TagDto>>(await _tagRepository.GetAllAsync());
        }

        public async Task<TagDto> GetByIdAsync(int id)
        {
            var tag = await _tagRepository.GetByIdAsync(id);
            if (tag == null) throw new KeyNotFoundException($"Tag with ID {id} not found.");
            return _mapper.Map<TagDto>(tag);
        }

        public async Task<PaginationResponse<TagDto>> GetPaginateAsync(int page, int take)
        {
            var products = _tagRepository.GetAllForPagination(null);
            int totalItemCount = products.Count();

            var paginated = products
                .Skip((page - 1) * take)
                .Take(take)
                .ToList();

            var mappedDatas = _mapper.Map<IEnumerable<TagDto>>(paginated);

            int totalPage = (int)Math.Ceiling((decimal)totalItemCount / take);

            return new PaginationResponse<TagDto>(mappedDatas, totalPage, page, totalItemCount);
        }
    }
}
